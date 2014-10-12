using UnityEngine;
using System.Collections;

public class CamAimCtl : MonoBehaviour 
{
	AimMove _aimMove;
	Transform aim;
	public bool timeScale=false;

	// Use this for initialization
	void Start () 
	{
		aimMove.max_v=1000;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(aim!=null)
		{
			aimMove.Aim(aim.localPosition,true);
			float v= (aim.localPosition-transform.localPosition).magnitude*3f;
			aimMove.v=v;
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

	public void Aim(Transform aim)
	{
		this.aim=aim;
	}
}
