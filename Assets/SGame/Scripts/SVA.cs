using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using System;



public class BaseValType
{

	public int atk;					
	public int def;						
	public float move_speed;		
	public float atk_rate;		
	public List<Vector2>ss=new List<Vector2>();
	public int[] lax;
	public Dictionary<string,int> ppox=new Dictionary<string, int>();
	public SortedList<string,int> exx=new SortedList<string, int>();

}


public class EEXX:MonoBehaviour
{
	 int def;						
	 float move_speed;		

	private string abc(int a,float b)
	{
		Debug.Log(a);
		Debug.Log(b);
		return (a+b).ToString();
	}

}

/// <summary>
/// 文本值解析
/// </summary>
public class SVA 
{

	public SVA()
	{
		BaseValType bvt=new BaseValType();
		bvt.atk_rate=123f;
		bvt.ss.Add(new Vector2(34f,51f));
		bvt.lax=new int[]{2,5,6,5};
		bvt.ppox.Add("eeex",500);
		bvt.ppox.Add("ex",5200);
		bvt.exx.Add("e1x",1234);
/*
		EEXX ex=new EEXX();
		Type t= ex.GetType();
		MethodInfo mi= t.GetMethod("abc",BindingFlags.NonPublic | BindingFlags.Public);
		object[] objs=new object[]{100,100.5f};
		string str=(string)(mi.Invoke(ex,objs));*/

		//FieldInfo fi= t.GetField("def");
		//fi.SetValue(ex,100);
		//int value=(int)(fi.GetValue(ex));

	
		//ScriptInterpreter.GetValue(bvt,"ss[0]");
		//Debug.Log( (float)ScriptInterpreter.GetValue(bvt,"ss[0].x"));
		//	Debug.Log( (float)ScriptInterpreter.GetValue(bvt,"ss[0].y"));
		//Debug.Log( (int)ScriptInterpreter.GetValue(bvt,"lax[2]"));
		//ScriptInterpreter.GetValue(bvt,"ppox['eeex']");
		//ScriptInterpreter.GetValue(bvt,"exx['e1x']");

		//Debug.Log( ((float)ScriptInterpreter.GetValue(bvt,"ss[0]")).ToString());

	}

}

