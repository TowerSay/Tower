using UnityEngine;
using System.Collections;

public class EffectControl : MonoBehaviour 
{
	public string effName="";
	Animator ani;


	void Start () 
	{
		ani=GetComponentInChildren<Animator>();

		if(effName!="")
		{
			//取得效果物体控制器
			ani.runtimeAnimatorController=Game.EAtr(effName);
		}
	}

	void Update () 
	{

	}


}
