using UnityEngine;
using System.Collections;

public class ArrowCtl : MonoBehaviour
{
	AimMove _aimMove;
	Vector3 LastPos;
	public float range;
	float addRange=0;

	void Start ()
	{
		LastPos=this.transform.localPosition;
	}

	public void OnAtk()
	{
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(LastPos!=this.transform.localPosition)
		{
			addRange+=(this.transform.localPosition-LastPos).magnitude;

			if(addRange>=range)
			{
				Destroy(this.gameObject);
			}

			LastPos=this.transform.localPosition;
		}


	}

	public AimMove aimMove
	{
		get
		{
			if(_aimMove=null)
			{
				_aimMove=GetComponent<AimMove>();
			}
			return _aimMove;
		}
	}
}
