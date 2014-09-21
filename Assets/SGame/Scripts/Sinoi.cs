using UnityEngine;
using System.Collections;

public static class Sinoi
{
	public static int FPS;
	public static float world_timer;

	static Sinoi()
	{
		
	}


/// <summary>
/// 可进行物免率计算(提供护甲值,预算值,限定值),以及所有限定值计算
/// </summary>
	static public float LVC(float Def,float Rot=0.8f,float Dor=100f)
	{	
		return ((1f-1f/(1f+Def/Rot))*Dor);
	}

}
