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


public class TCPClient :GameSocket
{
	public override void GetPackage (EndPoint remoteEP,short id, byte[] data)
	{

	}

	public TCPClient(string ip,int point,SocketType socketType=SocketType.Stream,ProtocolType protocolType=ProtocolType.Tcp)
	{
		this.ip=ip;
		this.point=point;
		this.protocolType =protocolType;
		this.socketType=socketType;
	}
}
