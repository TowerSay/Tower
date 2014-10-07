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
		Game.CreateSpriteClipPack("Actor1",new Point(32,32));

		for(int i=0;i<10;i++)
		{
			CharaCtlFourFace chr = Game.Instantiate<CharaCtlFourFace>(Game.CObj("Chara"));
			chr.transform.parent=charaPanel;
			chr.transform.localPosition=new Vector3(i*32-0.5f*10*32,GameHelp.Random(-100,100),0);
			chr.transform.localScale=Vector3.one;
			chr.id=new Point(GameHelp.Random(0,2)*3+1,1);

			chr.aimMove.v=GameHelp.Random(10,100);
			chr.rbc.weight=GameHelp.Random(100,200);



			chrs.Add(chr);
		}

		foreach(CharaCtlFourFace chr in chrs)
		{
			chr.aim=chrs[GameHelp.Random(0,chrs.Count-1)].transform;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
