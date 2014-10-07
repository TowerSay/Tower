using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefBoxCtl : MonoBehaviour 
{
	public static SortedList<int,DefBoxCtl> dbc_slst=new SortedList<int,DefBoxCtl>();
	public F f;

	void Start () 
	{
		dbc_slst.Add(GetInstanceID(),this);
		f=F.CreateF(transform.parent,Vector3.zero,0.9f,true);
	}

	void OnDestroy()
	{
		dbc_slst.Remove(GetInstanceID());
	}

	void Update () 
	{

		//transform.parent.localPosition=transform.localPosition;
	}

	void OnTriggerStay(Collider csi) 
	{
		int id=csi.GetInstanceID();
		//if(id!=this.GetInstanceID())
		{
			DefBoxCtl dbc=csi.GetComponent<DefBoxCtl>();

			
			Vector3 v3=(transform.parent.localPosition-csi.transform.parent.localPosition);
			
			dbc.f.CombineF(-v3.normalized*10f);
		//	Debug.Log(csi.name);
		}
	}



}
