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


public class P2PServer :GameSocket
{
	public override void GetPackage (EndPoint remoteEP,short id, byte[] data)
	{
		Debug.Log("客户:"+ remoteEP.ToString() +"\n包ID："+id+"\n包长度："+data.Length);
	}

	public P2PServer(int point,SocketType socketType=SocketType.Dgram,ProtocolType protocolType=ProtocolType.Udp)
	{
		this.point=point;
		this.protocolType =protocolType;
		this.socketType=socketType;
	
	}


	 public void Start ()
	{
		if(clientSocket!=null && clientSocket.Connected)return ;

		//服务器端口
		IPEndPoint ipEndpoint = new IPEndPoint (IPAddress.Any,point);
		//创建Socket对象
		clientSocket = new Socket (AddressFamily.InterNetwork,socketType,protocolType);

		//这是一个异步的建立连接，当连接建立成功时调用connectCallback方法
		//IAsyncResult result = clientSocket.BeginConnect (ipEndpoint,new AsyncCallback (connectCallback),clientSocket);
		
		//这里做一个超时的监测，当连接超过5秒还没成功表示超时
		//bool success = result.AsyncWaitHandle.WaitOne( 5000, true );

		//绑定网络地址
		clientSocket.Bind(ipEndpoint);
		Debug.Log("This is a Server, host name is {"+Dns.GetHostName()+"}");

			//与socket建立连接成功，开启线程接受服务端数据。
			//worldpackage = new List<JFPackage.WorldPackage>();
			thread = new Thread(new ThreadStart(ReceiveSorket));
			thread.IsBackground = true;
			thread.Start();

	}

	protected override void ReceiveSorket ()
	{
		//在这个线程中接受服务器返回的数据
		while (true)
		{ 
			try
			{

				if (clientSocket.Available <= 0) continue; 
				if (clientSocket == null) return;

				Debug.Log("收到包大小:");

				//接受数据保存至bytes当中
				byte[] bytes = new byte[clientSocket.ReceiveBufferSize];

				IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
				EndPoint remoteEP = (EndPoint)(sender);

				//List<Byte> byte_lst=new List<byte>();

				int size = clientSocket.ReceiveFrom(bytes,ref remoteEP);

				//Debug.Log("缓冲区大小:"+clientSocket.ReceiveBufferSize);
				/*
				while(size > 0)
				{
					for(int i=0;i<size;i++)
					{
						byte_lst.Add(bytes[i]);
					}
					break;
					bytes = new byte[1024];
					size = clientSocket.ReceiveFrom(bytes,ref remoteEP);
				

				}*/

			//	Debug.Log("收到包大小:"+byte_lst.Count);
				//Receive方法中会一直等待服务端回发消息
				//如果没有回发会一直在这里等着。
				//int i = clientSocket.ReceiveFrom(bytes,ref remoteEP);

				if(size> 0)
				{

					if(size > 4)
					{
						SplitPackage(remoteEP,bytes,0);
					}
					else
					{
						Debug.Log("包长度需要大于4");
					}

				}	

				
			}

			catch (Exception e)
			{
				Debug.Log("Failed to clientSocket error." + e);
				//ClientSocketClose();
				break;
			}
		}



	}

	//关闭Socket
	public override void Closed()
	{
		if(clientSocket != null)
		{

			ClientSocketClose();
		//	clientSocket.Shutdown(SocketShutdown.Both);
		}
		clientSocket = null;
	}

}
