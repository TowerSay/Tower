using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
//using UnityEditorInternal;
using System;
using System.Text;

public class AnimaEditor : EditorWindow
{
	AnimationClip ani_clip=null;
	GameObject aim=null;
	float fps_sec; 				//    	秒/帧率
	int max_frame;				//   	总帧
	List<AnimationEvent> ani_events;
	string copy;

	bool edit_more=false;

	[MenuItem ("Game/动画事件面板")]
	static void Init() 
	{

		//ani_events=new List<AnimationEvent>();
		AnimaEditor window = (AnimaEditor)EditorWindow.GetWindow (typeof (AnimaEditor));
		EditorHelp.InitEditorResource();
		//window.position = new Rect(0,0,150,30);
		window.title="动画事件面板";

		//window.Show();
		//window.autoRepaintOnSceneChange=true;
	}

	void Awake ()
	{

		this.ani_clip=EditorHelp. ac;
		//SVA sva=new SVA();


		//EditorHelp.InitEditorResource();
	}

	void OnProjectChange()
	{

		Repaint();
	//	EditorHelp.InitEditorResource();
	}

	/// <summary>
	/// 载入动画剪辑
	/// </summary>
	void LoadAnimation(AnimationClip ani_clip)
	{
		this.ani_clip=ani_clip;
		ReloadAniEventList();
	}

	/// <summary>
	/// 重读事件列表
	/// </summary>
	void ReloadAniEventList()
	{
		if(ani_clip!=null)
		{

			Debug.Log("reload");
			AnimationEvent[] ae=(AnimationUtility.GetAnimationEvents(ani_clip));
			if(ae!=null)
				ani_events=EditorHelp.CopyAnimaEventArrayToList(ae );

			fps_sec=1f/ani_clip.frameRate;
			max_frame=Mathf.CeilToInt( ani_clip.length*ani_clip.frameRate);
		}

	}

	/// <summary>
	/// 保存动画事件列表
	/// </summary>
	void SaveAniEventList()
	{

		AnimationUtility.SetAnimationEvents(ani_clip,EditorHelp.CopyAnimaEventListToArray(ani_events));
		EditorUtility.SetDirty(ani_clip);
	}

	void OnSceneGUI () 
	{

		//HandleUtility.
		//Handles.BeginGUI(new Rect(0,0,1000,1000));
		//Vector3 v3= HandleUtility.WorldToGUIPoint(aim.transform.position);
	//	Handles.Label(aim.transform.position,"asdasdasd");
		//Handles.EndGUI();
		//Handles.Label(Target.EFR+new Vector3(0,0.05f,0),Handles.currentCamera.fieldOfView.ToString());
		//Gizmos.DrawGUITexture(,
	}

	void OnInspectorUpdate()
	{
	//	Debug.Log( EditorHelp.GetFrame());
		EditorHelp. Reload();
		EditorHelp. RenewAc();
		//ReloadAniEventList();
		Repaint();
	}	

	void OnSelectionChange()
	{

/*
		if(Selection.objects.Length>0)
		{
			
			AnimationUtility.StartAnimationMode(Selection.objects);

		}*/

		//AnimationUtility.StartAnimationMode(Selection.objects);
		//AnimationClipSettings acs=	AnimationUtility.GetAnimationClipSettings();
		/*
		if(Selection.activeObject!=null)
		{
			AnimationClip ac=Selection.activeObject as AnimationClip;
			if(ac!=null)
			{
				edit_more=false;
				LoadAnimation(ac);
				Repaint();
			}

			GameObject go=Selection.activeObject as GameObject;
		
			if(go!=null)
			{
				aim=go;
				AnimationClip[] acs=AnimationUtility.GetAnimationClips(go);
				if(acs!=null)
				{
					edit_more=true;
				}
				//HandleUtility.WorldPointToSizedRect(go.transform.position,new GUIContent("阿斯顿"),GUIStyle.none);
				//HandleUtility.Repaint();
				/*
				float r=0;
				//Debug.Log(go.animation);
				AnimationClip[] acs=AnimationUtility.GetAnimationClips(go);

				for(int i=0;i<acs.Length;i++)
				{
					AnimationClipCurveData accd;
					//accd.propertyName
				//	Debug.Log(acs[i].name+" : "+r);
					//Debug.Log(acs[i].+" : "+r);
				}*/

				/*
				EditorCurveBinding[] cbs=AnimationUtility.GetAnimatableBindings(go,go);
				for(int i=0;i<cbs.Length;i++)
				{
				
					AnimationUtility.GetFloatValue(go,cbs[i],out r);
					Debug.Log(cbs[i].propertyName+" : "+r);
				}*/



			
		//	Debug.Log(Selection.activeObject.name);
		
	}

	void Update()
	{
		if(EditorHelp.ac!=null)
			if(ani_clip==null || (EditorHelp.ac.name!=ani_clip.name))
		{
			//EditorHelp.Reload();
			LoadAnimation(EditorHelp. ac);Repaint();return;
		}

		if(AnimationUtility.InAnimationMode())
		{


			//AnimationUtility.g
		}
		/*
		if(ani_clip!=null)
		if(AnimationUtility.InAnimationMode())
		{
			AnimationUtility.get
			ani_clip.apparentSpeed
			Debug.Log(ani_clip.apparentSpeed);
		}*/

		/*
		if(AnimationUtility.InAnimationMode())
		{
			AnimationUtility.GetFloatValue(
			EditorCurveBinding ecb=new EditorCurveBinding();
			ecb.path="ee";
			ecb.propertyName="exex";

			//AnimationUtility.GetEditorCurve(ani_clip,ecb);
		}*/
	}

	void AddStartEvent()
	{
		bool first=false;
		if(ani_events.Count>0)first=true;

		AnimationEvent s_ae=new AnimationEvent();
		s_ae.functionName="_start";s_ae.time=0;
		s_ae.stringParameter=ani_clip.name;
		if(!first)ani_events.Add(s_ae);
		else ani_events.Insert(0,s_ae);
	}

	void AddEndEvent()
	{
		AnimationEvent  s_ae=new AnimationEvent();
		s_ae.functionName="_end";s_ae.time=ani_clip.length;
		ani_events.Add(s_ae);
	}

	//事件标准化
	void CheckEventNT()
	{

		//GetWindow<AnimationWindow>();

		int last_id=ani_events.Count-1;
		
		for(int i=0;i<ani_events.Count;i++)
		{
			AnimationEvent ae=ani_events[i];
			if((ae.functionName=="_start" && i!=0) 
			   || (ae.functionName=="_end" && i!=last_id))
			{
				ani_events.Remove(ae);
			}
		}
		last_id=ani_events.Count-1;
		if(ani_events.Count>1)
		{
			//Debug.Log(last_id);
			if(ani_events[last_id].functionName!="_end")
			{AddEndEvent();}
			else ani_events[last_id].stringParameter="";
			
			if(ani_events[0].functionName!="_start")
			{AddStartEvent();}
			else ani_events[0].stringParameter=ani_clip.name;
		}
		else 
		{
			ani_events.Clear();
			AddStartEvent();
			AddEndEvent();
		}
		
		SaveAniEventList();

	}
	  

	void OnGUI()
	{
		//GUILayout.Label(EditorWindow.focusedWindow.ToString());
		//AnimationUtility.onCurveWasModified.BeginInvoke(ani_clip,null,null,null,null).IsCompleted
		if(AnimationUtility.InAnimationMode())
		{

			//aw.time;
	
			//AnimationWindow aw = (AnimationWindow)EditorWindow.GetWindow (typeof (AnimationWindow));
			//AnimationWindow aw=GetWindow<AnimationWindow>();
			//Debug.Log(aw.chosenClip);
			/*
			if(GUIUtility.hotControl!=0)
			{
				AnimationClip ac=GUIUtility.GetStateObject(typeof(AnimationClip),GUIUtility.hotControl) as AnimationClip;
				if(ac!=null)Debug.Log(ac.name);
 
				//Debug.Log(Event.current.type);
				Debug.Log("id: " + GUIUtility.hotControl);
				//Debug.Log(GUIUtility.GetControlID(FocusType.Passive));
			//	Event.current.Use();
			}*/
			//AnimationUtility.
		}

	//	Debug.Log( Event.current.ToString());
		float ady=4;
		float adx=4;



		if(ani_clip)
		{
			if(ani_events==null)ReloadAniEventList();

			if(GUI.Button(new Rect(adx,ady,48,20),"格式化"))
			{
				CheckEventNT();
			}
			adx+=48;
			//GUI.Label(new Rect(adx,ady,position.width-4-adx,20),EditorWindow.focusedWindow.title);

			//if(EditorWindow.focusedWindow.title=="UnityEditor.AnimationWindow")
			{
				if(Event.current.isMouse && Event.current.button==0 &&  Event.current.type==EventType.MouseDown)
				{
					EditorHelp.Reload();
					EditorHelp.GetAniWin();



				
				}
			

				//typeof(UnityEdit or.UnityEditor.AnimationWindow).type
				
				//Debug.Log(EditorWindow.focusedWindow.GetType().Namespace);
				//Debug.Log(EditorWindow.focusedWindow.GetType().IsPublic);
				//Debug.Log(this.GetType().Namespace);
				//Debug.Log(this.GetType().FullName);


			}

			adx=4;ady=24;
			GUI.Label(new Rect(adx,ady,position.width-4-adx,20),"剪辑:\t"+ani_clip.name,EditorStyles.largeLabel);//adx+=42;
			//ani_clip.name=GUI.TextField(new Rect(adx,ady,position.width-4-adx,18),ani_clip.name,EditorStyles.largeLabel);
			ady+=20;adx=4;
			GUI.Label(new Rect(adx,ady,36,20),"帧率:\t",EditorStyles.largeLabel);adx+=36;
			string rate=GUI.TextField(new Rect(adx,ady,32,18),ani_clip.frameRate.ToString(),EditorStyles.largeLabel);
			float rt=0;	
			if(float.TryParse(rate,out rt)){ani_clip.frameRate=rt;}
			adx+=36;//ady+=20;

			GUI.Label(new Rect(adx,ady,position.width-4-adx,20),"长度:"+ani_clip.length.ToString("f2")+"s\t"+max_frame+"帧"+"\t循环:"+ani_clip.isLooping + "_"+ani_clip.wrapMode,EditorStyles.largeLabel);
			adx=4;ady+=20;
			//GUI.Label(new Rect(adx,ady,80,20),"平均持续时间:"+ani_clip.averageDuration,EditorStyles.largeLabel);
			//GUI.Label(new Rect(adx,ady,80,20),"总帧:"+max_frame,EditorStyles.largeLabel);adx=4;ady+=20;
			//GUI.Label(new Rect(adx,ady,80,20),"角速率 :"+ani_clip.averageAngularSpeed,EditorStyles.largeLabel);adx+=80;
			//GUI.Label(new Rect(adx,ady,200,20),"平均速率:"+ani_clip.averageSpeed,EditorStyles.largeLabel);ady+=20;
			//GUI.Toggle(new Rect(adx,ady,200,20),ani_clip.isLooping,"循环播放");
			//GUI.Label(new Rect(adx,ady,120,20),);adx=4;
			ady+=24;

			EditorHelp.DrawXLine(new Rect(adx,ady+3,position.width-4-adx,1),new Color(1,1,1,0.3f));
			int num=max_frame%60;
			float aex=(position.width-8-adx)/num;

			for(int i=0;i<num+1;i++)
			{
				EditorHelp.DrawYLine(new Rect(adx,ady+4,1,(i%5==0)?16:8),new Color(1,1,1,0.5f));
				adx+=aex;
			}

			GUI.contentColor=Color.yellow;
			adx=aex*EditorHelp.frame+4;
			EditorHelp.DrawYLine(new Rect(adx,ady+4,1,11),Color.yellow);
			string str=(EditorHelp.frame)+"F";
			//float exlx= ((aex+8+str.Length*8>position.width)?position.width-str.Length*8-4:aex+4);
			GUI.Label(new Rect(adx,ady-24,36,14),str,EditorStyles.miniLabel);

			str=((EditorHelp.frame/((float)max_frame))*ani_clip.length).ToString("f2")+"s";
			//exlx= ((aex+8+str.Length*6>position.width)?position.width-str.Length*6-4:aex+4);
			GUI.Label(new Rect(adx,ady+24,36,14),str,EditorStyles.miniLabel);
			GUI.contentColor=Color.white;
			adx=4;

			float exw=36;
			for(int i=0;i<ani_events.Count;i++)
			{
				AnimationEvent ae=ani_events[i];

				aex=(ae.time/ani_clip.length)*(position.width-8-adx);
				EditorHelp.DrawYLine(new Rect(aex+4,ady+4,1,18),new Color(1,1,1,1));
				EditorHelp.DrawSecDef(new Rect(aex+4-4,ady+4,9,9),"mini");
				bool res=(i%2==0);

				 str=(int)((ae.time/ani_clip.length)*max_frame)+"F";
				float exlx= ((aex+8+str.Length*8>position.width)?position.width-str.Length*8-4:aex+4);
				GUI.Label(new Rect(exlx,ady-14,exw,14),str,EditorStyles.miniLabel);

				str=ae.time.ToString("f2")+"s";
				exlx= ((aex+8+str.Length*6>position.width)?position.width-str.Length*6-4:aex+4);
				GUI.Label(new Rect(exlx,res?ady+14:ady+24,exw,14),str,EditorStyles.miniLabel);
			}



			ady+=2;
			//GUI.Label
			GUI.color=new Color(1,1,1,0.35f);
			adx=2;
			GUI.Box(new Rect(adx,ady,position.width-2-adx,position.height-2-ady),"",EditorStyles.miniButton);
			ady+=38;
			GUI.color=Color.white;
		//	GUI.Label(new Rect(adx,ady,200,20),"isAnimatorMotion:"+ani_clip.isAnimatorMotion);ady+=20;
	//		GUI.Label(new Rect(adx,ady,200,20),"isHumanMotion:"+ani_clip.isHumanMotion);ady+=20;

		//	GUI.Label(new Rect(adx,ady,200,20),"localBounds:"+ani_clip.localBounds);ady+=20;

			if(Event.current.isKey &&Event.current.keyCode==KeyCode.Return)
			{
				//Debug.Log("xx");
				Repaint();
			}

			for(int i=0;i<ani_events.Count;i++)
			{
				AnimationEvent ae=ani_events[i];
				 str=ae.stringParameter;
				int size=str.Split('\n').Length;
				size=size*13+4;
				bool res=(ae.functionName!="_start" && ae.functionName!="_end");

	
				GUI.color=new Color(1,1,1,res?0.5f:0.6f);
				GUI.Label(new Rect(adx,ady+(res?0:2),(res?position.width-4-adx:60),(res?24+size:20)),"",EditorStyles.miniButton);
				GUI.color=Color.white;

				if(!res)
				{
					bool res2=(ae.functionName=="_start");
					GUI.color=res2?new Color(0.3f,1f,0.3f,1):new Color(0.2f,0.8f,1,1);

					GUI.Label(new Rect(adx,ady+4,60,20)," "+(res2?"Start":"End"));//ady+=20;
					GUI.color=Color.white;
					//ady+=2;
				}
				else
				{

					GUI.Label(new Rect(adx,ady,200,20),ae.functionName);

					GUI.Label(new Rect(position.width-44,ady,40,20),(ae.time/ani_clip.length)*max_frame+"F");ady+=20;adx=4;
					//GUI.Label(new Rect(adx,ady,200,20),"事件参数:"+ae.stringParameter);ady+=20;
					int pre_id=str.IndexOf("[int]");
					if(pre_id!=-1)
					{
						
						str.Remove(pre_id,4);
						//str.Insert(pre_id,"xxxw");
					}

					string nxe=GUI.TextArea(new Rect(adx+2,ady,position.width-10-adx,size),str,EditorStyles.numberField);
					
					if(nxe!=str)
					{
						ae.stringParameter=nxe;
						SaveAniEventList();
					}
				}



				ady+=size+6;
			}

		//	GUI.Label(new Rect(adx,ady,200,20),"localBounds:"+ani_clip.localBounds);ady+=20;


		}



		/*
		EventType eventType=  Event.current.type;
		if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform)
		{
			// Show a copy icon on the drag
			//拖动时显示辅助图标
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
		
			if (eventType == EventType.DragPerform)
			{
				DragAndDrop.AcceptDrag();
				
				if(DragAndDrop.objectReferences .Length>0)
				{
					if(DragAndDrop.paths.Length>0)
					{
						string[] fn=DragAndDrop.paths;
						List<string> strs=new List<string>() ;
						
						for(int i=0;i<fn.Length;i++)
						{
							string str=fn[i];
							if(str.Contains(".png"))
							{
								strs.Add(fn[i]);
							}
						}
						AnimationClip ac=DragAndDrop.objectReferences[0] as AnimationClip;
						LoadAnimation(ac);
						if(strs.Count>0)
						{


							string path=strs[0];
							int pos=path.LastIndexOf('/');
							path=	path.Remove(pos,path.Length-pos);
							fn=new string[strs.Count];
							for(int i=0;i<strs.Count;i++)
							{
								fn[i]=strs[i];
							}
							
							//Debug.Log(path);
							
							//TexLoad(fn,path);
						}
						
					}
				}
				else
				{
					if(DragAndDrop.paths.Length>0)
					{
						string path=DragAndDrop.paths[0];
						//StartLoad(path);
					}
				}
				
				DragAndDrop.PrepareStartDrag ();
			}
			
			
			Event.current.Use();
		}
*/

	}
}
