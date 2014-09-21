using UnityEngine;
using System.Collections;

public class BattleSceneManager : MonoBehaviour 
{
	public GameObject grd;
	public GameObject sky;
	private Material grd_mt,sky_mt;

	void Start () 
	{

		grd_mt= grd.GetComponent<MeshRenderer>().material;
		sky_mt= sky.GetComponent<MeshRenderer>().material;
	}
	

	void Update () 
	{
		grd_mt.mainTextureOffset+=new Vector2(1f*Game.RealDeltaTime(),0);
		sky_mt.mainTextureOffset+=new Vector2(0.03f*Game.RealDeltaTime(),0);
	}
}
