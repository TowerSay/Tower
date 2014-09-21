using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class AnimatorControlHelp : MonoBehaviour
{
	ActControlFourFace act;
	Animator ani;
	public string ani_name;
	public int circle=0;
	public bool end=false;
	public float end_timer=0f;

	void Start () 
	{
		act=GetComponentInParent<ActControlFourFace>();
		if(ani==null)ani=transform.GetComponent<Animator>();
	}

	void _start(string name)
	{
		if(ani_name!=name)
		{
			circle=0;
			ani_name=name;
		}
		else circle++;
	}

	void _end()
	{
		end=true;
	}

	void Event(AnimationEvent ae)
	{

	}
	
	void Update () 
	{
		if(end)
		{
			if(end_timer>0.01f)
			{
				end=false;
				end_timer=0;
			}
			else
			{
				end_timer+=Game.RealDeltaTime();
			}
		}
	}
}
