using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BattleSRTM : SRTMBase
{
	public Transform charaPanel;
	List<CharaCtlFourFace>chrs=new List<CharaCtlFourFace>();

	// Use this for initialization
	void Start () 
	{
		for(int i=0;i<10;i++)
		{
			CharaCtlFourFace chr = Game.Instantiate<CharaCtlFourFace>(Game.CObj("Chara"));
			chr.transform.parent=charaPanel;
			chr.transform.localPosition=new Vector3(i*32,0,0);
			chr.transform.localScale=Vector3.one;
			chr.aimMove.v=GameHelp.Random(10,100);
			chr.rbc.weight=GameHelp.Random(100,200);
			chrs.Add(chr);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
