using UnityEngine;
using System.Collections;

public class LoginSRTM : SRTMBase 
{
	
	void Start () 
	{
	
	}

	void Update () 
	{
		
	}

	public void BtnSinglePlayer()
	{
		Game.RTM.p2pServer.Start();

		Game.RTM.p2pClient.Connect();


		SocketPakage.WorldPackage wp=new SocketPakage.WorldPackage();
		Game.RTM.p2pClient.SendMessage(1001,wp);


		//Game.RTM.tcpClient.Connect();
		//SocketPakage.WorldPackage wp=new SocketPakage.WorldPackage();
		//Game.RTM.p2pClient.SendMessage(1001,wp);

	}
}
