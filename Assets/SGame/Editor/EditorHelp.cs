using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
using System;
using System.Text;

public  class EditorBase<T> : Editor where T : UnityEngine.Object
{
	protected T Target { get { return  (T)target; } }
}


public static class AnimaWinHelp
{
	//public static MethodInfo[] mis;
	public static Dictionary<string,MethodInfo>func;


}


public static class EditorHelp
{

	static Dictionary<string,Texture2D> texs;
	public static Type AniWinType;				
	public static EditorWindow ew;
	public static AnimationClip ac;
	public static FieldInfo fi_ac;
	public static object sl_state_obj;
	public  static object o;
	public static Assembly asmb;
	public static float frame;

	static EditorHelp()
	{

	}

	public static Type GetAniWin()
	{
		

		Debug.Log("调用一次____________:"+AniWinType.FullName);
		return AniWinType;
	}

	public static void RenewAc()
	{

		//Debug.Log(ac);
		if(o!=null)
		if(fi_ac!=null && sl_state_obj!=null)
		{
			sl_state_obj=  o.GetType().GetMethod("get_state").Invoke(o,null);
			fi_ac= sl_state_obj.GetType().GetField("m_ActiveAnimationClip",BindingFlags.Public | BindingFlags.NonPublic                  
			                                       | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			ac= fi_ac.GetValue(sl_state_obj) as AnimationClip;

			//Debug.Log(ac);
		}

	}

	public static float GetFrame()
	{
		loadUnityEditorAssembly();
		
		//float frame=(float) (sl_state_obj.GetType().GetField("m_Frame",BindingFlags.Public | BindingFlags.NonPublic                  
		     //                                       | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly).GetValue(sl_state_obj));
		return 0f;
		//return frame;
	}

	private static void loadUnityEditorAssembly()
	{
		if (asmb == null)asmb = Assembly.Load("UnityEditor") ;
		if (AniWinType == null)AniWinType=asmb.GetType("UnityEditor.AnimationWindow");
		if (o == null)	o= EditorWindow.GetWindow(AniWinType);
		if (sl_state_obj == null)	sl_state_obj=  o.GetType().GetMethod("get_state").Invoke(o,null);
	}

	public static void Reload()
	{
		//Debug.Log("XSX");
		//if (asmb == null)
		{ 
			loadUnityEditorAssembly();
			//object eo= System.Activator.CreateInstance(AniWin);

			//if(AniWinType!=null)
			{
				//rnc_lst

				if(fi_ac==null)fi_ac= sl_state_obj.GetType().GetField("m_ActiveAnimationClip",BindingFlags.Public | BindingFlags.NonPublic                  
				                                       | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
				ac= fi_ac.GetValue(sl_state_obj) as AnimationClip;

				frame=(Int32)(sl_state_obj.GetType().GetField("m_Frame",BindingFlags.Public | BindingFlags.NonPublic                  
				                                      | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly).GetValue(sl_state_obj));
				/*
				FieldInfo[] fis= sl_state_obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic                  
				                                 | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

				for(int i=0;i<fis.Length;i++)
				{
					//UnityEditor.TimeArea ta;ta.vSlider

					FieldInfo fi=fis[i];
					//AnimationWindow aw;aw.state.
				//	Debug.Log(fi.GetValue(sl_state_obj).GetType().ToString()+" "+fi.Name+" "+fi.GetValue(sl_state_obj));
				}*/


				//wd.positon="x";
				//Debug.Log(AniWin.GetMember("state")[0].Name);

				//AnimationWindowState aws=(AnimationWindowState)asmb.GetType("UnityEditor.AnimationWindow.state");
				
				
				//asmb.CreateInstance(AniWin.FullName,false,BindingFlags.Public|BindingFlags.Instance, null, new object[] { "OK" }, null, null);
				//EditorWindow ew=(EditorWindow)AniWin;
				
				
				//	AnimationWindow aw;aw.state.m_ActiveAnimationClip
			//	if(ew!=null)
			//		Debug.Log("转换成功:"+ew.position);
				/*

				FieldInfo[] fis= sl_ew.GetFields(BindingFlags.Public | BindingFlags.NonPublic                  
				                                 | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

				for(int i=0;i<fis.Length;i++)
				{
					FieldInfo fi=fis[i];

					Debug.Log(fi.Name+" "+fi.GetValue(o));
				}
				*/
			//	Debug.Log("_____________________");
			//	AnimationWindowState aws;aws.

				//当前选择的

			//	Debug.Log(ac.name);
				/*
				MethodInfo[] mes= sl_ew.GetMethods();
				for(int i=0;i<mes.Length;i++)
				{
					//AnimationWindow aw;aw.state.
					string str="函数:";
					MethodInfo me=mes[i];
				//	AnimaWinHelp.func.Add(me.Name,me);

					//me.Invoke(null,null);

					str+=(me.IsStatic?"static ":"")+ (me.IsPublic?"public":"private")+" "+me.ReturnType+" "+me.Name+"( ";
					foreach(ParameterInfo p in me.GetParameters())
					{
						 
						str+=p.ParameterType+"  "+p.Name+" , ";
						//Debug.Log(p.ParameterType+"  "+p.Name);
					}
					str+=")";
					Debug.Log(str);
				}

				FieldInfo rnc_lstfi=sl_ew.GetField("m_ChosenClip",BindingFlags.Public | BindingFlags.NonPublic                  
				                              | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
				object rnc_lstfi_clip=rnc_lstfi.GetValue(o);
				Type rnc_lstfi_clip_type=rnc_lstfi_clip.GetType();
				
				FieldInfo[] fis= rnc_lstfi_clip_type.GetFields(BindingFlags.Public | BindingFlags.NonPublic                  
				                                 | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
				for(int i=0;i<fis.Length;i++)
				{
					FieldInfo fi=fis[i];
			
					Debug.Log(fi.Name+" "+fi.GetValue(o));
				}*/
				//Debug.Log(rnc_lstfi.GetValue(o));
				//FieldInfo fi=sl_ew.GetField("position");

				//FieldInfo fi= AniWin.GetField("position");
				//Debug.Log(fi.GetValue(fi));
				//AnimationWindowState aws;aws.m_ActiveAnimationClip
				//Type rnc_lst=(AniWin.GetMethod("get_state").Invoke(AniWin,null)).GetType();
				//Debug.Log( rnc_lst.GetMember("frameRate")[0].MemberType);

			}
		}
	}

	public static Texture2D Tex(string name)
	{
		if(texs==null)InitEditorResource();
		if(!texs.ContainsKey(name)){LoadTex(name);}
		return texs[name];
	}

	/// <summary> 在编辑器OnGUI上绘制精灵</summary>
	public static void EditorDrawSprite(Rect rect,Sprite spt)
	{
		Vector2 offset= new Vector2( spt.rect.width/(float)spt.texture.width,spt.rect.height/(float)spt.texture.height);
		Vector2 pos=new Vector2( spt.rect.x/(float)spt.texture.width,spt.rect.y/(float)spt.texture.height);
		Rect _rect=new Rect(pos.x,pos.y,offset.x,offset.y);
		GUI.DrawTextureWithTexCoords(rect,spt.texture,_rect);
	}

	/// <summary>
	/// 检查并处理动画剪辑的始末事件.
	/// </summary>
	/// <param name="ac">Ac.</param>
	/// <param name="create"> <c>true</c>:创建模式 <c>   false</c>:清除模式  </param>
	public static void CheckAnimationClipEvent(AnimationClip ac,bool create=true)
	{
		AnimationEvent[] ae_ary = AnimationUtility.GetAnimationEvents(ac);
		List<AnimationEvent> ae_rnc_lstt=new List<AnimationEvent>(ae_ary);

		//移除 _start && _end 事件
		for(int o=0;o<2;o++)
		for(int i=0;i<ae_rnc_lstt.Count;i++)
		{
			AnimationEvent ae = ae_rnc_lstt[i];
			if(ae.functionName=="_start" && ae.time==0)
			{
				ae_rnc_lstt.RemoveAt(i);
			}
			else if(ae.functionName=="_end" && ae.time==ac.length)
			{
				ae_rnc_lstt.RemoveAt(i);
			}
		}

		//添加 _start && _end 事件
		if(create)
		{
			AnimationEvent e=new AnimationEvent();
			e.functionName="_start";
			e.time=0f;
			e.stringParameter=ac.name;
			ae_rnc_lstt.Add(e);

			e=new AnimationEvent();
			e.functionName="_end";
			e.time=ac.length;
			ae_rnc_lstt.Add(e);
		}

		ae_ary=GameHelp.ListToArray<AnimationEvent>(ae_rnc_lstt);
		AnimationUtility.SetAnimationEvents(ac,ae_ary);
	}


	static void LoadTex(string name)
	{
		if(name==".")
		{Texture2D px=new Texture2D(1,1);px.SetPixel(0,0,Color.white);px.Apply();
		texs["."]=px;}
		else
		texs[name]=Resources.LoadAssetAtPath<Texture2D>("Assets/SGame/Resources/"+name+".png");
	}

	public static void DrawSecDef(Rect rect,Color color,string str="def")
	{
		GUI.color=color;
		GUI.DrawTexture(rect,Tex("sec"+str));

		GUI.color=Color.white;
	}

	public static List<AnimationEvent> CopyAnimaEventArrayToList(AnimationEvent[] aes)
	{
		List<AnimationEvent> rnc_lst=new List<AnimationEvent>();
		foreach( AnimationEvent ae in aes)
		{
			rnc_lst.Add(ae);
		}
		return rnc_lst;
	}

	public static AnimationEvent[] CopyAnimaEventListToArray(List<AnimationEvent> aes)
	{
		AnimationEvent[] rnc_lst=new AnimationEvent[aes.Count];
		for(int i=0;i<aes.Count;i++)
		{
			rnc_lst[i]= aes[i];
		}
		return rnc_lst;
	}

	public static void DrawSecDef(Rect rect,string str="def")
	{
		DrawSecDef(rect,Color.white,str);
	}


	public static void DrawXLine(Rect rect,Color color)
	{
		GUI.color=color;
		GUI.DrawTexture(rect,Tex("xline"));
		GUI.color=Color.white;
	}

	public static void DrawYLine(Rect rect,Color color)
	{
		GUI.color=color;
		GUI.DrawTexture(rect,Tex("yline"));
		GUI.color=Color.white;
	}

	public static void DrawPBox(Rect rect,Color color)
	{
		GUI.color=color;
		GUI.DrawTexture(rect,Tex("."));
		GUI.color=Color.white;
	}

	public static void InitEditorResource()
	{
		if(texs==null)texs=new Dictionary<string, Texture2D>();
	}
}
