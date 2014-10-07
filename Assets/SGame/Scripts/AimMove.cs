using UnityEngine;
using System.Collections;

/// <summary>
///  目标移动工具类 
/// </summary>
public class AimMove : MonoBehaviour 
{
	public Vector3 aim;					//目标位置

	public enum TYPE
	{
		TRSPOS,		//位置偏移
		RGBDF			//刚体恒力
	}

	public TYPE type=TYPE.TRSPOS;

	public float v=1;						//恒定速度

	private Vector3 add;				//位移增量
	public bool move=false;			//移动状态
	public float max_v=5;				//最大位移速度（紧作用于RGBDF模式）
	private bool move_loop; 		//true:始终沿原方向移动 false:目标位置停止移动
	public bool update=true;		//更新移动
	bool local_position;

	Rigidbody _rigidBody;

	float local_rate=1f;

	public bool justXY=false;

	public Rigidbody rigidBody
	{
		get
		{
			if(_rigidBody==null){_rigidBody=GetComponent<Rigidbody>();}return _rigidBody;
		}
	}

	public float Angle
	{
		get
		{
			Vector2 v2=new Vector2(aim.x-Position.x,aim.y-Position.y);
			float angle=Vector2.Angle(Vector2.up,v2);
			if(aim.x<Position.x)
			{
				angle=360-angle;
			}
			return angle;
		}
	}


	void Start()
	{
		if (type==TYPE.RGBDF && rigidBody == null)
		{
			type=TYPE.TRSPOS;
		
		}
	}

#if UNITY_EDITOR
	void OnGUI()
	{
		/*
		Color c= new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),1f);

		if(local_position)
		{
			Vector3 v3=transform.position;
			transform.localPosition=aim;
			Debug.DrawLine(v3,transform.position,c);
			transform.position=v3;

		}else Debug.DrawLine(transform.position,aim,c);
*/
		//Debug.Log("xx");
	}
#endif

	public Vector3 GetAimPos()
	{
		return aim;
	}

	void Update () 
	{
		if (update)
		{

			if(move)
			{
				Vector3 pos=Position;
				if(type==TYPE.RGBDF){pos=rigidBody.velocity;}

				if(move_loop)
				{
					if(type==TYPE.RGBDF)
					{

						if(pos.magnitude<max_v*local_rate)rigidBody.AddForce(add*100f);
					}
					else if(type==TYPE.TRSPOS)
					{
						Position+=add;
					}
				}
				else
				{
					Renew();
					Vector3 l=aim-Position;

					if(type==TYPE.RGBDF)
					{
						if(l.magnitude<rigidBody.velocity.magnitude*local_rate*Game.RealDeltaTime() )
						{
							Position=aim;
							move=false;
							add=Vector3.zero;
						}
						else
						{
							if(pos.magnitude<max_v*local_rate)rigidBody.AddForce(add*100f);
						}
					}
					else if(type==TYPE.TRSPOS)
					{
						if(l.magnitude<add.magnitude)
						{
							Position=aim;
							move=false;
							add=Vector3.zero;
						}
						else
						{
							Position+=add;
						}
					}
				}

			}
		}




	}

	Vector3 Position
	{
		get
		{
			if(local_position)
			{
				return transform.localPosition;
			}
			else return transform.position;
		}
		set
		{
			if(local_position)
			{
				transform.localPosition=value;
			}
			else transform.position=value;
		}
	}


	//重算单帧位移向量
	void Renew()
	{
		add=aim-Position;
		add=add.normalized*v*Game.RealDeltaTime();
	}

	/// <summary>
	/// 设定目标位置
	/// </summary>
	/// <param name="v3">V3.</param>
	/// <param name="MoveLoop">If set to <c>true</c> move loop.</param>
	public void Aim(Vector3 v3,bool local_position=false,bool move_loop=false)
	{
		if(local_position)
		{
			Vector3 rh=this.transform.position;
			transform.localPosition=new Vector3(1,0,0);
			local_rate=1f/transform.position.x;
			transform.position=rh;
		}else local_rate=1f;

		this.move_loop = move_loop;
		move=true;
		aim=v3;

		Renew();
		this.local_position=local_position;


	}

}