using UnityEngine;
using System.Collections;

public class CharaActControl : MonoBehaviour 
{
	AimMove _aimMove;
	Animator _ani;

	public enum State
	{

	}

	public int team;


	public AimMove aimMove
	{
		get
		{
			if(_aimMove==null){_aimMove=GetComponent<AimMove>();}return _aimMove;
		}
	}

	public Animator ani
	{
		get
		{
			if(_ani==null){_ani=GetComponentInChildren<Animator>();}return _ani;
		}
	}


	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!aimMove.move)
		{
			aimMove.Aim(this.transform.position+Vector3.right*(Random.Range(-2,3)));
		}

	}
}
