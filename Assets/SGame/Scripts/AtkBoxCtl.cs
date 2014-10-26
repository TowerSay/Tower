using UnityEngine;
using System.Collections;

public class AtkBoxCtl : MonoBehaviour 
{
	public float f=100;
	public float weight=50;
	public Transform parent;
	public Transform root;

	public string tagType;

	void Start () 
	{
		if(parent==null)
		{
			parent=this.transform;
		}
		if(root==null)
		{
			root=parent;
		}
	}

	void Update () 
	{
		
	}

	void Base(RigBoxCtl rbc)
	{
		float rate=weight/rbc.weight;
		Vector3 v3=(parent.localPosition-rbc.parent.localPosition);
		rbc.f.CombineF(-v3.normalized*f*10*rate);

	}

	//攻击检
	void OnTriggerEnter(Collider csi)
	{

		if(csi.tag=="RigBox" && this.tag=="AtkBox" )
		{
			RigBoxCtl rbc=csi.GetComponent<RigBoxCtl>();
			int id=rbc.parent.GetInstanceID();

			if(id!=root.GetInstanceID())
			{
				CharaCtlFourFace emy=rbc.parent.GetComponent<CharaCtlFourFace>();
				if(root!=null)
				{
					CharaCtlFourFace chr=root.GetComponent<CharaCtlFourFace>();
					
					if(chr!=null)
					{
						chr.Atk(emy);
						emy.aim=chr;
					}
				}

				if(tagType=="Chara" && rbc.tagType=="Chara")
				{

					Base(rbc);
				}
				else if(tagType=="Arrow" && rbc.tagType=="Chara")
				{
					Base(rbc);
					parent.SendMessage("OnAtk",emy,SendMessageOptions.DontRequireReceiver);
				}

			}
		}

	}
}
