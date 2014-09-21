using UnityEngine;
using System.Collections;

public class AnimatiorHelp : MonoBehaviour 
{
	public string aniName;
	public bool timeScaleEnable=true;
	public bool playEnd=false;

	Animator ani;

	float worldTimeScale=1f;
	float aniSpeed=1f;
	bool playEndCheck=false;
	float playEndCheckTimer=0f;

	void Start () 
	{
		ani=GetComponent<Animator>();
		worldTimeScale=Game.worldTimeScale;
		aniSpeed=ani.speed;
	}

	void Update () 
	{
		if(timeScaleEnable)
		{
			if(worldTimeScale!=Game.worldTimeScale)
			{
				ani.speed=Game.WorldScale(aniSpeed);
			}
		}

		if(playEndCheck)
		{
			if(playEndCheckTimer>0)
			{
				playEndCheckTimer=0;
				playEndCheck=false;
				playEnd=false;
			}
			else playEndCheckTimer+=Game.RealDeltaTime(timeScaleEnable);
		}

	}

	void _start(string ani_name)
	{
		this.aniName=ani_name;
		playEndCheck=true;
		playEndCheckTimer=0f;
	}

	void _end()
	{
		playEnd=true;
	}
}
