using UnityEngine;
using System.Collections;

public enum AtkType
{
	Slashing=1,
	Piercing=2,
	Shooting=3,
}

public class WeponCtl : MonoBehaviour 
{
	public AtkType atkType=AtkType.Piercing;

	UI2DSprite _spt;
	Animator _ani;

	public float atkRange;
	/// <summary>所在的精灵包</summary>
	SpriteClipPack _scp; 
	public SpriteClipPack scp
	{
		get
		{
			if(_scp==null)
			{
				_scp=Game.GetSpriteClipPack("Wepon");
			}
			return _scp;
		}
	}


	// Use this for initialization
	void Start () 
	{
		RenewWepon();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void RenewWepon()
	{
		if(atkType==AtkType.Piercing)
		{
			atkRange=48f;
			spt.sprite2D=scp.spts[new Point(3,1)];
		}
		else if(atkType==AtkType.Shooting)
		{
			atkRange=500f;
			spt.sprite2D=scp.spts[new Point(1,1)];
		}
	}

	public void RenewAngle(float angle)
	{
		transform.localEulerAngles=new Vector3(0,0,360-angle);
	}

	public bool CheckToAtk(float range,float atkSpeed)
	{
		if(range<atkRange)
		{

			PlayAtk();
			ani.speed=atkSpeed;
			return true;
		}

		return false;
	}


	public void PlayAtk()
	{
		if(atkType==AtkType.Piercing)
		{
			ani.Play("Atk");
		}
		else if(atkType==AtkType.Shooting)
		{
			ani.Play("Shoot");
		}
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
