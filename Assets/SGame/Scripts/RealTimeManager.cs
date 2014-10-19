using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Data;

public class RealTimeManager : MonoBehaviour 
{
	public AudioSource audio_source;
	public List<Thread> threads=new List<Thread>();

	public P2PClient p2pClient;
	public P2PServer p2pServer;
	public TCPClient tcpClient;

	// Use this for initialization
	void Start () 
	{
	

		p2pClient=new P2PClient("127.0.0.1",3005);
		p2pServer=new P2PServer(3005);

		Game.RTM=this;
	//	t=new Thread(Program.Init);
	//	t.Start();
		//StartCoroutine(PRI());
		GameObject.DontDestroyOnLoad(this.gameObject);
		//Game.PlaySound("Bell1");

		DbAccess.OpenDB("Db.db");
		IDataReader idr= DbAccess.SQL("SELECT * FROM Subx");

		if(idr.Read())
		{
			Debug.Log(idr.GetString(idr.GetOrdinal("Name")));
		}
		//IDataReader idr= DbAccess.SQL("");
		//idr.GetByte(idr.GetOrdinal());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnDestroy()
	{

		DbAccess.CloseDB();

		if(p2pServer!=null)p2pServer.Closed();
		if(p2pClient!=null)p2pClient.Closed();
		if(tcpClient!=null)tcpClient.Closed();

		foreach(Thread t in threads)
		{
			t.Abort();
		}
	}

}
