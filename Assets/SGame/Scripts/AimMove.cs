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

	Rigidbody _rigidBody;

	public Rigidbody rigidBody
	{
		get
		{
			if(_rigidBody==null){_rigidBody=GetComponent<Rigidbody>();}return _rigidBody;
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
		Color c= new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),1f);
		Debug.DrawLine(transform.position,aim,c);
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
				Vector3 pos=transform.position;
				if(type==TYPE.RGBDF){pos=rigidBody.velocity;}

				if(move_loop)
				{
					if(type==TYPE.RGBDF)
					{

						if(pos.magnitude<max_v)rigidBody.AddForce(add*100f);
					}
					else if(type==TYPE.TRSPOS)
					{
						transform.position+=add;
					}
				}
				else
				{
					Renew();
					Vector3 l=aim-transform.position;

					if(type==TYPE.RGBDF)
					{
						if(l.magnitude<rigidBody.velocity.magnitude*Game.RealDeltaTime() )
						{
							transform.position=aim;
							move=false;
							add=Vector3.zero;
						}
						else
						{
							if(pos.magnitude<max_v)rigidBody.AddForce(add*100f);
						}
					}
					else if(type==TYPE.TRSPOS)
					{
						if(l.magnitude<add.magnitude)
						{
							transform.position=aim;
							move=false;
							add=Vector3.zero;
						}
						else
						{
							transform.position+=add;
						}
					}
				}

			}
		}




	}

	//重算单帧位移向量
	void Renew()
	{
		add=aim-transform.position;
		add=add.normalized*v*Game.RealDeltaTime();
	}

	/// <summary>
	/// 设定目标位置
	/// </summary>
	/// <param name="v3">V3.</param>
	/// <param name="MoveLoop">If set to <c>true</c> move loop.</param>
	public void Aim(Vector3 v3,bool move_loop=false)
	{
		this.move_loop = move_loop;
		move=true;
		aim=v3;
		Renew();
	}

}