using UnityEngine;
using System.Collections;

public class CharaInfo : MonoBehaviour 
{

	/// <summary>名称 </summary>
	[SerializeField]
	string name="";

	/// <summary>护甲</summary>
	[SerializeField]
	int armor = 75;

	///<summary>伤害</summary>
	[SerializeField]
	int damage = 25;
	
	///<summary>移动速度</summary>
	[SerializeField]
	float moveSpeed = 0.25f;



	/// <summary>曲线 </summary>
	[SerializeField]
	AnimationCurve curve= AnimationCurve.Linear(0,0,10,10);




	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
