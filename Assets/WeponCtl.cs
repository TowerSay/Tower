using UnityEngine;
using System.Collections;

public class WeponCtl : MonoBehaviour 
{

	UI2DSprite _spt;
	Animator _ani;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public UI2DSprite spt
	{
		get
		{
			if(_spt==null)
			{
				_spt=transform.Find("Ani/Spt"). GetComponent<UI2DSprite>();
			}
			return _spt;
		}
	}

	public Animator ani
	{
		get
		{
			if(_ani==null)
			{
				_ani=transform.Find("Ani"). GetComponent<Animator>();
			}
			return _ani;
		}
	}
}
