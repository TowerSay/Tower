using UnityEngine;
using System.Collections;

public class ActControlFourFace : MonoBehaviour 
{
	bool air;
	public Animator ani;
	AimMove aim_move;
	AnimatorControlHelp ach;

	public enum STATE
	{
		IDLE,
		MOVE,
		RUN,

	}

	public enum FACE
	{
		LEFT=1,
		RIGHT
	}

	public STATE last_state=STATE.IDLE;
	public STATE state=STATE.IDLE;
	public FACE face=FACE.RIGHT;

	// Use this for initialization
	void Start ()
	{
		ach=GetComponentInChildren<AnimatorControlHelp>();
		ani=GetComponentInChildren<Animator>();
		aim_move=GetComponent<AimMove>();
	}


	void Play()
	{
		//ani.speed=Game.wolrd_time;
	}

	void Facing(FACE face)
	{
		if(this.face!=face)
		{
			Vector3 v3=this.transform.localEulerAngles;
			v3.y=(face==FACE.LEFT)?180:0;
			this.transform.localEulerAngles=v3;
			this.face=face;
		}
	

	}

	// Update is called once per frame
	void Update ()
	{
		Vector2 id=Vector2.zero;

		if(Input.GetKey(KeyCode.D))
		{
			id.x=1;
			Facing(FACE.RIGHT);
		}
		else if(Input.GetKey(KeyCode.A))
		{
			id.x=-1;
			Facing(FACE.LEFT);
		}

		if(Input.GetKey(KeyCode.W))
		{
			id.y=1;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			id.y=-1;	
		}

		if(id!=Vector2.zero)
		{
			Vector3 v3=new Vector3(id.x,0,id.y);	v3.Normalize();
			
			aim_move.Aim(transform.position+v3*1f);
			state=STATE.MOVE;
			//rigidbody.AddForce();
		}
		else
		{
			state=STATE.IDLE;
			aim_move.v=10f;

		}

		ani.speed=aim_move.v/10f;

		if(state==last_state)
		{
			if(ach.end)if(aim_move.v<20f)aim_move.v+=3f;
		}
		else last_state=state;

		switch(state)
		{
		case STATE.IDLE:
			ani.Play("idle");

			break;
		case STATE.MOVE:

			if(aim_move.v>13f)
			{
				ani.Play("run"); 
			}else ani.Play("move");
			break;
		}

		//Input.anyKey
	}


}
