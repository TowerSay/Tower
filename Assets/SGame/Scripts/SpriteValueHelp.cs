using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpriteValueHelp : MonoBehaviour
{
	SpriteRenderer sr;
	public float layer;
	private int last_id;
	// Use this for initialization
	void Start () 
	{
		if(sr==null)sr=GetComponent<SpriteRenderer>();
	}

	void Awake()
	{
		if(sr==null)sr=GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(last_id!=(int)layer)
		{
			layer=(float)((int)layer);
			sr.sortingOrder=(int)layer;
			last_id=(int)layer;
		}

	}
}
