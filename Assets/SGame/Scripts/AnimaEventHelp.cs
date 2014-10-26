using UnityEngine;
using System.Collections;

public class AnimaEventHelp : MonoBehaviour 
{
	public CharaCtlFourFace chrCtl;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ExcuteInstruct(string instuct)
	{

		if(instuct.Contains("CreateArrow"))
		{
			string[] str=instuct.Split(' ');
			float range=float.Parse(str[1]);

			ArrowCtl arrowCtl=Game.Instantiate<ArrowCtl>(Game.EObj("Arrow"));
	

			arrowCtl.transform.parent=Game.SRTM<BattleSRTM>().charaPanel;
			arrowCtl.transform.localEulerAngles=new Vector3(0,0,360-chrCtl.angle);
			arrowCtl.range=range;
			arrowCtl.transform.localScale=Vector3.one;
			arrowCtl.transform.position=this.transform.position;
			arrowCtl.GetComponent<AtkBoxCtl>().root=chrCtl.transform;

			F.CreateF(arrowCtl.transform,arrowCtl.transform.up*500f,1f,true);
		}



	}



}
