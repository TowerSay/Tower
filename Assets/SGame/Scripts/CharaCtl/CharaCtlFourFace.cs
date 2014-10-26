using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("游戏/控制器/RPG四面向角色控制器")]
public class CharaCtlFourFace : MonoBehaviour
{
	/// <summary>基本角色信息</summary>
	public CharaBaseInfo info;

	WeponCtl _wp;

	UI2DSprite _spt;
	AimMove _aimMove;
	RigBoxCtl _rbc;

	float timer=0;

	/// <summary>面向</summary>
	public FACE face;
	/// <summary>面向角度</summary>
	public float angle;

	/// <summary>生存状态 false为死亡</summary>
	public bool living=true;

	/// <summary>目标</summary>
	public CharaCtlFourFace aim;

	/// <summary>所在的精灵包</summary>
	public SpriteClipPack scp; 
	/// <summary>所在的精灵包裁剪点</summary>
	public Point id;

	/// <summary>行走动画步伐</summary>
	public int step=0;
	/// <summary>行走动画步伐ID组</summary>
	static int[] step_ary=new int[]{0,1,2,1};

	public bool atkState=false;
	public float atkTimer=0;

	public float atkSpeed=1f;

	///<summary>面向枚举<summary>
	public enum FACE
	{
		LEFT=1,
		RIGHT=2,
		UP=3,
		DOWN=0
	}

	public WeponCtl wp
	{
		get
		{
			if(_wp==null)
			{
				_wp=transform.Find("Wepon").GetComponent<WeponCtl>();
			}
			return _wp;
		}
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

		scp=Game.GetSpriteClipPack("Actor1");

	}

	///<summary>面向ID<summary>
	public int FaceInt
	{
		get
		{
			return (int)face;
		}
	}

	///<summary>改变面向<summary>
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
			float rate=(100f/aimMove.v)*0.5f+0.5f;

			if(timer>0.25f*rate)
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

	public void Atk(CharaCtlFourFace emy)
	{
		emy.info.HP-=this.info.Atk-emy.info.Def;
	}

	void FindNewAim()
	{
		BattleSRTM Scene=Game.SRTM<BattleSRTM>();

		int id=-1;
		float maxRange=float.MaxValue;

		for(int i=0;i< Scene.chrs.Count;i++)
		{
			CharaCtlFourFace chr =  Scene.chrs[i];
			if(chr.GetInstanceID()!=this.GetInstanceID())if(chr.living)
			{
				float range=(chr.transform.localPosition-this.transform.localPosition).magnitude;
				if(range<maxRange)
				{
					maxRange=range;
					id = i;
				}
			}

		}

		if(id!=-1)
		this.aim=Scene.chrs[id];
	}

	void Update () 
	{

		if(info.HP==0 && living)
		{
			aimMove.update=false;
			aimMove.move=false;

			rbc.enabled=false;
			living=false;

			GameObject go= GameObject.Instantiate(Game.EObj("DieEff")) as GameObject;

			go.transform.parent=Game.SRTM<BattleSRTM>().charaPanel;
			go.transform.localPosition=this.transform.localPosition;
			go.transform.localScale=Vector3.one;

			Destroy(this.gameObject);
			Game.SRTM<BattleSRTM>().chrs.Remove(this);
		}

		if(aim!=null)
		{
			if(!aim.living)
			{
				aim=null;
			}

		}

		spt.depth=-(int)transform.localPosition.y;
		wp.spt.depth=spt.depth+1;

		
		if(living)
		{

			if(aim!=null)
			{
				Vector3 dis=aim.transform.localPosition-transform.localPosition;
				Vector3 aimPos=aim.transform.localPosition;

				if(wp.atkType==AtkType.Shooting)
				{
					aimPos=dis.normalized*(dis.magnitude-wp.atkRange);
				}



				aimMove.Aim(aimPos,true);

				Vector2 v2=new Vector2(dis.x,dis.y);
				angle=Vector2.Angle(Vector2.up,v2);if(dis.x<0){angle=360-angle;};
				wp.RenewAngle(angle);

				if(!atkState)
				{
					if(atkTimer>1f/atkSpeed)
					{
						atkState=true;
						atkTimer=0f;
					}
					else atkTimer+=Game.RealDeltaTime(true);
				}
				else
				{
					float range=dis.magnitude;
					//angle=aimMove.Angle;

		
					if(wp.CheckToAtk(range,atkSpeed))
					{
						atkState=false;
					}
				}
			}
			else
			{
				angle=aimMove.Angle;

				FindNewAim();

				if(aim==null)
				if(!aimMove.move)
				{
					aimMove.v=GameHelp.Random(50,80);
					aimMove.Aim(transform.localPosition+new Vector3(Random.Range(-50,50),Random.Range(-50,50),0),true);
				}

			}

			
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
