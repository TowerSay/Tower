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


public class P2PClient :GameSocket
{
	public override void GetPackage (EndPoint remoteEP,short id, byte[] data)
	{

	}

	public P2PClient(string ip,int point,SocketType socketType=SocketType.Dgram,ProtocolType protocolType=ProtocolType.Udp)
	{
		this.ip=ip;
		this.point=point;
		this.protocolType =protocolType;
		this.socketType=socketType;
	}
}
