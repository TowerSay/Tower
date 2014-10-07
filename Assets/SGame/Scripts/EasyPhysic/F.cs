using UnityEngine;
using System.Collections;

///<summary>基本力<summary>
public class F : MonoBehaviour 
{
	public static F CreateF(Transform trs,Vector3 f,float damp=0.8f,bool localState=false)
	{
		F _f=trs.gameObject.AddComponent<F>();
		
		_f.damp=damp;
		_f.f=f;
		_f.localState=localState;
		return _f;
	}

	public Vector3 f;
	public float damp=0.8f;

	public bool localState=false;
	float timer=0f;

	void Start () 
	{
		
	}

	public void CombineF(Vector3 f)
	{
		this.f+=f;
	}

	Vector3 Position
	{
		get
		{
			if(localState)
			{
				return transform.localPosition;
			}
			else return transform.position;
		}
		set
		{
			if(localState)
			{
				transform.localPosition=value;
			}
			else transform.position=value;
		}
	}

	void Update () 
	{
		Position+=f*Game.RealDeltaTime(true);
		if(timer>0.05f)
		{
			f*=damp;
			timer=0;
		}else timer+=Game.RealDeltaTime(true);



	}
}
