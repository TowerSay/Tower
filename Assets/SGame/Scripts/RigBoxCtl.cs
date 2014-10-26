using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 外体控制器
/// </summary>
public class RigBoxCtl : MonoBehaviour 
{
	public static SortedList<int,RigBoxCtl> rbc_slst=new SortedList<int,RigBoxCtl>();
	public F f;

	/// <summary>重量 </summary>
	public float weight=100;		//标准100kg
	
	/// <summary> 帧位移速度 </summary>
	public float speed;

	/// <summary>上帧位置 </summary>
	Vector3 lastPos;

	/// <summary> 帧位移 </summary>
	Vector3 dev;
	public string tagType;

	void Start () 
	{
		rbc_slst.Add(GetInstanceID(),this);
		f=F.CreateF(transform.parent,Vector3.zero,0.8f,true);

		dev=Vector3.zero;
		lastPos=transform.parent.localPosition;
	}

	void OnDestroy()
	{
		rbc_slst.Remove(GetInstanceID());
	}

	
	public Transform parent
	{
		get
		{
			return transform.parent;
		} 
	}

	void Update () 
	{
		dev=transform.parent.localPosition-lastPos;
		lastPos=transform.parent.localPosition;
		speed=dev.magnitude*10f;
	}

	//基本形体碰撞
	void OnTriggerStay(Collider csi) 
	{
		int id=csi.GetInstanceID();
		if(id!=this.GetInstanceID())
		{

			if(csi.tag=="RigBox" && this.tag=="RigBox")
			{
				RigBoxCtl dbc=csi.GetComponent<RigBoxCtl>();
				float rate=weight/dbc.weight;
				
				Vector3 v3=(transform.parent.localPosition-csi.transform.parent.localPosition);
				
				dbc.f.CombineF(-v3.normalized*(speed+10f)*rate);
			}

			//Debug.Log(csi.name);
		}

	}



}
