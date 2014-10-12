using UnityEngine;
using System.Collections;

public class TimeLiving : MonoBehaviour 
{
	public float livingTime=1f;


	void Update () 
	{
		if(livingTime>0)
		{
			livingTime-=Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
