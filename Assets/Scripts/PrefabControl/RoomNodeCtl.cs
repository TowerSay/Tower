using UnityEngine;
using System.Collections;

public class RoomNodeCtl : MonoBehaviour 
{
	bool _right=false;
	bool _down=false;
	UISprite _r_obj,_d_obj,_spt;

	public Point id;
	UILabel _idLabel;
	UILabel _typeLabel;
	public int depth=-1;

	void Start () 
	{

	}

	void Update () 
	{
		idLabel.text=id.x+","+id.y;
	}

	public UILabel idLabel
	{
		get
		{
			if(_idLabel==null)
			{_idLabel=transform.Find("ID").GetComponent<UILabel>();}
			return _idLabel;
		}
	}

	public UILabel typeLabel
	{
		get
		{
			if(_typeLabel==null)
			{_typeLabel=transform.Find("TYPE").GetComponent<UILabel>();}
			return _typeLabel;
		}
	}

	public UISprite spt
	{
		get
		{
			if(_spt==null)
			{_spt=GetComponent<UISprite>();}
			return _spt;
		}
	}

	public UISprite r_obj
	{
		get
		{
			if(_r_obj==null)
			{_r_obj=transform.Find("R").GetComponent<UISprite>();}
			return _r_obj;
		}
	}
	
	public UISprite d_obj
	{
		get
		{
			if(_d_obj==null)
			{_d_obj=transform.Find("D").GetComponent<UISprite>();}
			return _d_obj;
		}
	}


	public  bool right
	{
		get
		{
			return _right;
		}
		set
		{
			if(!_right && value){OpenRight();}
			else if(_right && !value)CloseRight();
			_right=value;
		}
	}

	public bool down
	{
		get
		{
			return _down;
		}
		set
		{
			if(!_down && value){OpenDown();}
			else if(_down && !value)CloseDown();
			_down=value;
		}
	}

	void OpenRight()
	{
		r_obj.alpha=1;
	}

	void CloseRight()
	{
		r_obj.alpha=0;
	}

	void OpenDown()
	{
		d_obj.alpha=1;
	}
	
	void CloseDown()
	{
		d_obj.alpha=0;
	}
}
