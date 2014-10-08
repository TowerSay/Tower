using UnityEngine;
using System.Collections;

public class AtkBoxCtl : MonoBehaviour 
{
	public float f=100;
	public float weight=20;


	void Start () 
	{
		
	}

	void Update () 
	{
	
	}

	public Transform parent
	{
		get
		{
			return transform.parent.parent.parent.parent;
		} 
	}

	//攻击检
	void OnTriggerStay(Collider csi)
	{

		if(csi.tag=="RigBox" && this.tag=="AtkBox" )
		{
			RigBoxCtl dbc=csi.GetComponent<RigBoxCtl>();
			int id=dbc.parent.GetInstanceID();
			if(id!=parent.GetInstanceID())
			{

				float rate=weight/dbc.weight;
				
				Vector3 v3=(parent.localPosition-dbc.parent.localPosition);

				dbc.f.CombineF(-v3.normalized*f*10*rate);
				Debug.Log(csi.name+":"+f);

				CharaCtlFourFace ccff=dbc.parent.GetComponent<CharaCtlFourFace>();
				if(ccff!=null)
				{
					ccff.aim=parent;
				}
			}
		}

	}
}
