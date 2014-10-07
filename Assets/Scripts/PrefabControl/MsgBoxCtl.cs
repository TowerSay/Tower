using UnityEngine;
using System.Collections;

public class MsgBoxCtl : MonoBehaviour
{
	UILabel _lb;
	string msg;
	public float time;

	public UILabel lb
	{
		get
		{
			if(_lb==null)
			{
				_lb=transform.Find("msg").GetComponent<UILabel>();
			}
			return _lb;
		}
	}

	void Start () 
	{
		
	}
	

	void Update () 
	{
		
	}

	public void SwapMsg(string msg)
	{
		this.msg=msg;
		lb.text=msg;
	}
}
