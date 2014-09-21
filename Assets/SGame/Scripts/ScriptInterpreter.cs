using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// 脚本解释器
/// </summary>
public static class ScriptInterpreter
{
	/// <summary> 取值 </summary> </summary>
	public static object GetValue(object aim,string inst)
	{
		return ReadClassInfo(aim,inst);
	}

	//混合运算
	static object MixedOperation(string inst)
	{
		return null;
	}

	//检测圆括号
	static object DetectionOfOarentheses(object aim,string inst)
	{
		return null;
	}
	
	private static object ReadClassInfo(object aim,string inst)
	{
		string[] vcl=inst.Split('.');
		string name=vcl[0];
		if(name=="")name=vcl[1];

		int pre_l=name.IndexOf('[');
		int pre_r=name.IndexOf(']');

		int id=-1;
		object key=null;

		if(pre_l!=-1)
		{
			name=name.Substring(0,pre_l);
			string sub=inst.Substring(pre_l+1,pre_r-pre_l-1);
			int sub_l=sub.IndexOf('\'');
			int sub_r=sub.LastIndexOf('\'');

			if(sub_l!=-1)
			{
				sub=sub.Substring(sub_l+1,sub_r-sub_l-1);
				key=sub;
			}
			else
			if(int.TryParse(sub,out id)){key=id;}
			else
			{

			}
		}


	//	Debug.Log(name);
		//Debug.Log(id);
		//Debug.Log(key_name);
		//Debug.Log(inst);
		if(aim!=null)
		{
			//Debug.Log(aim.GetType().FullName);
		}

		aim=aim.GetType().GetField(name).GetValue(aim);

		Type objType = aim.GetType();

		//Debug.Log(objType.Name);
		string full_name=objType.FullName;
		//Debug.Log(full_name);


		//objType.FullName.Contains(typeof(List<>).FullName)

		if (pre_l!=-1)
		{
			if(objType.Name.Contains("List`1"))
			{
				//IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(GetType(full_name)));
				IList list=aim as IList;

				//Debug.Log(list.Count);
				aim= list[id];

			}
			else if(objType.Name.Contains("[]"))
			{
				Array array=aim as Array;
				aim=array.GetValue(id);


			}
			else if(objType.Name.Contains("Dictionary`2"))
			{
				//Type[] type= GetDicType(full_name);
				//IDictionary dictionary = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(type[0], type[1]));
				IDictionary dictionary=aim as IDictionary;

				aim=dictionary[key] ;

			}
			else if(objType.Name.Contains("SortedList`2"))
			{
				//Debug.Log(key.GetType().FullName);
				//Type[] type= GetDicType(full_name);
				//IDictionary srnc_lstt = (IDictionary)Activator.CreateInstance(typeof(SortedList<,>).MakeGenericType(type[0], type[1]));
				IDictionary srnc_lstt=aim as IDictionary;
		
				aim=srnc_lstt[key] ;

			}
		}

		inst=inst.Remove(0,(vcl[0]=="")?inst.Length:vcl[0].Length);
		//Debug.Log(inst);

		if(inst.Length>0)
		{
			return ReadClassInfo(aim,inst);
		}

		return aim;
	}



	static Type  GetType(string full_name)
	{
		Type type=Type.GetType("");
		int f_l=full_name.IndexOf("[System.");
		if(f_l!=-1)
		{
			int f_l_d=full_name.IndexOf(',',f_l);
			string tn1= full_name.Substring(f_l+1,f_l_d-f_l-1);
			type=Type.GetType(tn1);
			Debug.Log(type.FullName);
		}
		return type;
	}

	static Type[] GetDicType(string full_name)
	{
		Type[] type=new Type[2];
		int f_l=full_name.IndexOf("[System.");
		int f_r=full_name.IndexOf("[System.",f_l+1);
		if(f_l!=-1)
		{
			int f_l_d=full_name.IndexOf(',',f_l);
			string tn1= full_name.Substring(f_l+1,f_l_d-f_l-1);
			int f_r_d=full_name.IndexOf(',',f_r);
			string tn2= full_name.Substring(f_r+1,f_r_d-f_r-1);
			type[0]=Type.GetType(tn1);
			type[1]=Type.GetType(tn2);
			Debug.Log(type[0].FullName);
			Debug.Log(type[1].FullName);
		}
		return type;
	}



}
