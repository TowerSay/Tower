using UnityEngine;
using System.Collections;

public class HpBindCharaCtl : MonoBehaviour 
{
	public enum State
	{
		Hidden,		//隐藏
		Appear,		//消失
		Disappear,	//出现
	}

	public State state=State.Hidden;

	UISpriteAutoLength _usal;
	CharaCtlFourFace _chr;
	Animator _ani;

	float timer;

	public float last_rate;

	// Use this for initialization
	void Start () 
	{
		last_rate=chr.info.HPRate;
	}

	void SwapState(State state)
	{
		switch(state)
		{
		case State.Hidden:
		{
			ani.Play("Hidden");
			break;
		}
		case State.Appear:
		{
			ani.Play("Appear");
			break;
		}		
		case State.Disappear:
		{
			ani.Play("Disappear",0,0);
			break;
		}
		}
		this.state=state;
	}

	// Update is called once per frame
	void Update () 
	{
		if(last_rate!=chr.info.HPRate)
		{

			SwapState(State.Disappear);
			last_rate=chr.info.HPRate;
		}

		usal.rate=chr.info.HPRate;
	}


	public Animator ani
	{
		get
		{
			if(_ani==null)
			{
				_ani=GetComponent<Animator>();
			}
			
			return _ani;
		}
	}

	public UISpriteAutoLength usal
	{
		get
		{
			if(_usal==null)
			{
				_usal=transform.Find("t").GetComponent<UISpriteAutoLength>();
			}

			return _usal;
		}
	}

	public CharaCtlFourFace chr
	{
		get
		{
			if(_chr==null)
			{
				_chr=transform.parent.GetComponent<CharaCtlFourFace>();
			}
			
			return _chr;
		}
	}

}
