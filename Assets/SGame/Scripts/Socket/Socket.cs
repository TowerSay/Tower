using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSocket
{
	protected string ip;
	protected int point;
	protected ProtocolType protocolType;
	protected SocketType socketType;
	
	//Socket客户端对象
	protected Socket clientSocket;
	//JFPackage.WorldPackage是我封装的结构体，
	//在与服务器交互的时候会传递这个结构体
	//当客户端接到到服务器返回的数据包时，我把结构体add存在链表中。

	//public List<JFPackage.WorldPackage> worldpackage;

	//单例模式
	private static GameSocket instance;
	protected Thread thread =null;

	public bool ConnectState
	{
		get
		{
			if(clientSocket!=null)
			return clientSocket.Connected;

			return false;
		}
	}

	public virtual void Connect()
	{
		if(clientSocket!=null && clientSocket.Connected)return ;

		//创建Socket对象
		clientSocket = new Socket (AddressFamily.InterNetwork,socketType,protocolType);
		//服务器IP地址
		IPAddress ipAddress = IPAddress.Parse (ip);
		//服务器端口
		IPEndPoint ipEndpoint = new IPEndPoint (ipAddress,point);
		//这是一个异步的建立连接，当连接建立成功时调用connectCallback方法
		IAsyncResult result = clientSocket.BeginConnect (ipEndpoint,new AsyncCallback (connectCallback),clientSocket);

		//这里做一个超时的监测，当连接超过5秒还没成功表示超时
		bool success = result.AsyncWaitHandle.WaitOne( 5000, true );

		
		if ( !success )
		{
			//超时
			Closed();
			Debug.Log("连接超时!");
		}
		else
		{
			Debug.Log("与socket建立连接成功");
			//与socket建立连接成功，开启线程接受服务端数据。
			//worldpackage = new List<JFPackage.WorldPackage>();
			thread = new Thread(new ThreadStart(ReceiveSorket));
			thread.IsBackground = true;
			thread.Start();
		}



	}


	protected void connectCallback(IAsyncResult asyncConnect)
	{

		Debug.Log("连接服务器成功!");
	}
	
	protected virtual void ReceiveSorket()
	{
		//在这个线程中接受服务器返回的数据
		while (true)
		{ 

			if ( !clientSocket.Connected)
			{
				//与服务器断开连接跳出循环
				Debug.Log("与服务器断开连接");

				ClientSocketClose();
				break;
			}
			try
			{
				
				if (clientSocket.Available <= 0) continue; 
				if (clientSocket == null) return;

				//接受数据保存至bytes当中
				byte[] bytes = new byte[4096];

				IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
				EndPoint remoteEP = (EndPoint)(sender);
				
				//Receive方法中会一直等待服务端回发消息
				//如果没有回发会一直在这里等着。
				int i = clientSocket.ReceiveFrom(bytes,ref remoteEP);

				if(i <= 0)
				{
					ClientSocketClose();
					break;
				}	
				
				//这里条件可根据你的情况来判断。
				//因为我目前的项目先要监测包头长度，
				//我的包头长度是2，所以我这里有一个判断
				if(bytes.Length > 4)
				{
					SplitPackage(remoteEP,bytes,0);
				}
				else
				{
					Debug.Log("包长度需要大于4");
				}
				
			}
			catch (Exception e)
			{
				Debug.Log("Failed to clientSocket error." + e);
				ClientSocketClose();
				break;
			}
		}
	}	

	public virtual void GetPackage(EndPoint remoteEP, short id,byte[] data)
	{
		Debug.Log(id+"data.l"+data.Length);
	}

	protected void SplitPackage(EndPoint remoteEP,byte[] bytes , int index)
	{
		//在这里进行拆包，因为一次返回的数据包的数量是不定的
		//所以需要给数据包进行查分。
		while(true)
		{
			//包ID是2个字节
			byte[] msgId=new byte[2];
			//包头是2个字节
			byte[] head = new byte[2];

			Debug.Log("拆包!");

			//把数据包的前两个字节拷贝出来
			Array.Copy(bytes,index,msgId,0,2);

			Debug.Log("Array.Copy(bytes,index,msgId,0,2)");

			Array.Copy(bytes,index+2,head,0,2);


			short id=(short)BytesToStruct(msgId,typeof(short));
			Debug.Log("id:"+id);

			//计算包头的长度
			short length = BitConverter.ToInt16(head,0);
			Debug.Log("length:"+length);

			int headLengthIndex = index + 4;


			//当包头的长度大于0 那么需要依次把相同长度的byte数组拷贝出来
			if(length > 0)
			{
				byte[] data = new byte[length];
				//拷贝出这个包的全部字节数
				Array.Copy(bytes,headLengthIndex,data,0,length);
				//把数据包中的字节数组强制转换成数据包的结构体
				//BytesToStruct()方法就是用来转换的
				//这里需要和你们的服务端程序商量，

				Debug.Log("获得包!");
				GetPackage(remoteEP,id,data);

				//SocketPakage.BasePakage wp = new SocketPakage.BasePakage();
				//wp = (SocketPakage.WorldPackage)BytesToStruct(data,wp.GetType());
				//把每个包的结构体对象添加至链表中。
			//	worldpackage.Add(wp);
				//将索引指向下一个包的包头
				index  =  headLengthIndex + length;
				
			}
			else
			{
				//如果包头为0表示没有包了，那么跳出循环
				break;
			}
		}
	}	
	
	//向服务端发送一条字符串
	//一般不会发送字符串 应该是发送数据包
	public void SendMessage(string str)
	{
		byte[] msg = Encoding.UTF8.GetBytes(str);
		
		if(!clientSocket.Connected)
		{
			ClientSocketClose();
			return;
		}
		try
		{
			//int i = clientSocket.Send(msg);
			IAsyncResult asyncSend = clientSocket.BeginSend (msg,0,msg.Length,SocketFlags.None,new AsyncCallback (sendCallback),clientSocket);
			bool success = asyncSend.AsyncWaitHandle.WaitOne( 5000, true );
			if ( !success )
			{
				ClientSocketClose();
				Debug.Log("Failed to SendMessage server.");
			}
		}
		catch
		{
			Debug.Log("send message error" );
		}
	}
	
	//向服务端发送数据包，也就是一个结构体对象
	public void SendMessage(short id,object obj)
	{

		if(!clientSocket.Connected)
		{
			ClientSocketClose();
			return;
		}
		try
		{
			byte [] msgId = BitConverter.GetBytes(id);
		
			//先得到数据包的长度
			short size = (short)Marshal.SizeOf(obj);
			//把数据包的长度写入byte数组中
			byte [] head = BitConverter.GetBytes(size);
			//把结构体对象转换成数据包，也就是字节数组
			byte[] data = StructToBytes(obj);
			
			//此时就有了两个字节数组，一个是标记数据包的长度字节数组， 一个是数据包字节数组，
			//同时把这两个字节数组合并成一个字节数组
			
			byte[] newByte = new byte[msgId.Length+ head.Length + data.Length];

			Array.Copy(msgId,0,newByte,0,msgId.Length);
			Array.Copy(head,0,newByte,msgId.Length,head.Length);
			Array.Copy(data,0,newByte,head.Length+msgId.Length, data.Length);


			//计算出新的字节数组的长度
			int length =  Marshal.SizeOf(id) + Marshal.SizeOf(size) + Marshal.SizeOf(obj);
			
			//向服务端异步发送这个字节数组
			IAsyncResult asyncSend = clientSocket.BeginSend (newByte,0,length,SocketFlags.None,new AsyncCallback (sendCallback),clientSocket);

			//监测超时
			bool success = asyncSend.AsyncWaitHandle.WaitOne( 5000, true );

			if ( !success )
			{
				clientSocket.Close();
				Debug.Log("Time Out !");
			} 
			
		}
		catch (Exception e)
		{
			Debug.Log("send message error: " + e );
		}
	}
	
	//结构体转字节数组
	public byte[] StructToBytes(object structObj)
	{
		
		int size = Marshal.SizeOf(structObj);
		IntPtr buffer =  Marshal.AllocHGlobal(size);
		try
		{
			Marshal.StructureToPtr(structObj,buffer,false);
			byte[]  bytes  =   new byte[size];
			Marshal.Copy(buffer, bytes,0,size);
			return   bytes;
		}
		finally
		{
			Marshal.FreeHGlobal(buffer);
		}
	}

	//字节数组转结构体
	public object BytesToStruct(byte[] bytes,Type  strcutType)
	{
		int size = Marshal.SizeOf(strcutType);
		IntPtr buffer = Marshal.AllocHGlobal(size);
		try
		{
			Marshal.Copy(bytes,0,buffer,size);
			return  Marshal.PtrToStructure(buffer,   strcutType);
		}
		finally
		{
			Marshal.FreeHGlobal(buffer);
		}   
		
	}
	
	protected void sendCallback (IAsyncResult asyncSend)
	{
		
	}

	protected void ClientSocketClose()
	{
		Debug.Log("Socket关闭!");
		if(clientSocket!=null)clientSocket.Close();
		if(thread!=null)thread.Abort();
	}

	//关闭Socket
	public virtual void Closed()
	{
		if(clientSocket != null && clientSocket.Connected)
		{
			clientSocket.Shutdown(SocketShutdown.Both);
			ClientSocketClose();
		}
		clientSocket = null;
	}
	
}

