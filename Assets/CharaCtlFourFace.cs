using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharaCtlFourFace : MonoBehaviour
{
	UI2DSprite _spt;
	AimMove _aimMove;
	RigBoxCtl _rbc;

	List<Sprite>spts=new List<Sprite>();
	static int[] step_ary=new int[]{0,1,2,1};
	float timer=0;

	public FACE face;
	public float angle;

	public SpriteClipPack scp; 
	public Point id;

	public int step=0;
	
	///<summary>面向枚举<summary>
	public enum FACE
	{
		LEFT=1,
		RIGHT=2,
		UP=3,
		DOWN=0
	}

	public UI2DSprite spt
	{
		get
		{
			if(_spt==null)
			{
				_spt=GetComponent<UI2DSprite>();
			}
			return _spt;
		}
	}

	public RigBoxCtl rbc
	{
		get
		{
			if(_rbc==null)
			{
				_rbc=transform.Find("RigBox").GetComponent<RigBoxCtl>();
			}
			return _rbc;
		}
	}

	public AimMove aimMove
	{
		get
		{
			if(_aimMove==null)
			{
				_aimMove=GetComponent<AimMove>();
			}
			return _aimMove;
		}
	}

	
	void Start () 
	{
		Game.CreateSpriteClipPack("Actor1",new Point(32,32));
		scp=Game.GetSpriteClipPack("Actor1");
		id=new Point(1,1);
	}

	///<summary>面向ID<summary>
	public int FaceInt
	{
		get
		{
			return (int)face;
		}
	}


	public void SwapFace(FACE face)
	{
		if(this.face!=face)
		{
			this.face=face;
			spt.sprite2D=scp.spts[new Point(step_ary[step],FaceInt)+id];
			timer=0;

		}
		else
		{
			if(timer>0.25f)
			{
				timer=0;
				step++;

				if(step>=step_ary.Length)
				{
					step=0;
				}

				spt.sprite2D=scp.spts[new Point(step_ary[step],FaceInt)+id];
			}else timer+=Game.RealDeltaTime(true);
		}


	}



	void Update () 
	{
		spt.depth=-(int)transform.localPosition.y;

		if(!aimMove.move)
		{
			aimMove.Aim(transform.localPosition+new Vector3(Random.Range(-50,50),Random.Range(-50,50),0),true);
			//aimMove.Aim(new Vector3(0,0,0),true);
		}
		else
		{
			angle=aimMove.Angle;
			if(angle>45 && angle<=135)
			{
				SwapFace(FACE.RIGHT);
			}
			else if(angle>135 && angle<=225)
			{
				SwapFace(FACE.DOWN);
			}
			else if(angle>225 && angle<=315)
			{
				SwapFace(FACE.LEFT);
			}
			else
			{
				SwapFace(FACE.UP);
			}
		}
	}
}
