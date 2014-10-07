using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Data;

public class RealTimeManager : MonoBehaviour 
{
	public AudioSource audio_source;
	public List<Thread> threads=new List<Thread>();

	// Use this for initialization
	void Start () 
	{
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
		foreach(Thread t in threads)
		{
			t.Abort();
		}
	}

}
