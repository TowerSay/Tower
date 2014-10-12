using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BattleSRTM : SRTMBase
{
	public Transform charaPanel;
	public List<CharaCtlFourFace>chrs=new List<CharaCtlFourFace>();

	public CamAimCtl camAim;

	// Use this for initialization
	void Start () 
	{
		Game.CreateSpriteClipPack("Actor1",new Point(32,32));
		int max=8;
		for(int i=0;i<max;i++)
		{
			CharaCtlFourFace chr = Game.Instantiate<CharaCtlFourFace>(Game.CObj("Chara"));
			chr.transform.parent=charaPanel;
			chr.transform.localPosition=new Vector3(i*32-0.5f*max*32,GameHelp.Random(-100,100),0);
			chr.transform.localScale=Vector3.one;
			chr.id=new Point(GameHelp.Random(0,3)*3+1,GameHelp.Random(0,1)*4+1);

			chr.aimMove.v=GameHelp.Random(10,100);
			chr.rbc.weight=GameHelp.Random(100,200);

			CharaBaseInfo info=new CharaBaseInfo();

			info.HP=info.MaxHP=GameHelp.Random(100,200);
			info.Atk=GameHelp.Random(10,20);
			info.Def=GameHelp.Random(1,5);

			chr.info=info;

			chrs.Add(chr);
		}

		int ranId=GameHelp.Random(0,chrs.Count-1);

		chrs[ranId].aimMove.v=200;
		chrs[ranId].rbc.weight=100;

		chrs[ranId].atkSpeed=2.5f;
		chrs[ranId].info.Atk=30;
		chrs[ranId].info.HP=chrs[ranId].info.MaxHP=200;

		camAim.Aim(chrs[ranId].transform);

		/*
		foreach(CharaCtlFourFace chr in chrs)
		{
			chr.aim=chrs[GameHelp.Random(0,chrs.Count-1)].transform;
		}*/

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
