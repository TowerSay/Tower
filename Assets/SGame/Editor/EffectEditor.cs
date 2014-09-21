using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
using System;
using System.Text;

public class EffectEditor : EditorWindow
{
	GameObject effObj;
	Animator effAni;

	List<string>s_rnc_lstt=new List<string>();
	List<AnimationClip>ac_rnc_lstt=new List<AnimationClip>();
	Dictionary<string,Sprite>spt_dic=new Dictionary<string, Sprite>();
	
	float timer=0f;
	bool checkSelection=false;
	GameObject selectionObj=null;

	[MenuItem ("Game/编辑器/特效物")]
	static void Init() 
	{
		EffectEditor window = (EffectEditor)EditorWindow.GetWindow (typeof (EffectEditor));
		window.title="特效物编辑器";
		window.Show();
	}

	void OnEnable ()
	{
		//创建临时的特效物体
		effObj=GameObject.Find("_Effect");
		if(effObj==null)
		{
			effObj=GameObject.Instantiate(Game.EObj("Effect")) as GameObject;
			effObj.name="_Effect";
		}
		if(effObj!=null)
		{
			effAni=effObj.GetComponentInChildren<Animator>();
		}
		ReLoadAniEffect();
	}

	void ReLoadAniEffect()
	{
		s_rnc_lstt.Clear();
		spt_dic.Clear();
		ac_rnc_lstt.Clear();

		//取得基本的动画剪辑模型
		//AnimationClip base_ac = Game.ERM.Res<AnimationClip>("BaseAnimation.anim");

		AnimationClip[] acp_ary=Resources.LoadAll<AnimationClip>("Prefab/EffObj/Animation/");
		ac_rnc_lstt=new List<AnimationClip>(acp_ary);
		//Debug.Log("数目:"+acp_ary.Length.ToString());
		//List<AnimatorController> ac_rnc_lstt=new List<AnimatorController>(ac_ary);

		//取剪辑关键帧上的精灵存入字典
		foreach(AnimationClip acp in acp_ary)
		{

			EditorCurveBinding[] ecb_ary=AnimationUtility.GetObjectReferenceCurveBindings(acp);
			for(int i=0;i<ecb_ary.Length;i++)
			{
				EditorCurveBinding ecb=ecb_ary[i];
				if(ecb.propertyName=="m_Sprite")
				{
					ObjectReferenceKeyframe[] rkf_ary= AnimationUtility.GetObjectReferenceCurve(acp,ecb);
					for(int o=0;o<rkf_ary.Length;o++)
					{
						ObjectReferenceKeyframe rkf=rkf_ary[o];
						Sprite spt=rkf.value as Sprite;

						if(spt!=null)
						{
							s_rnc_lstt.Add(acp.name);
							spt_dic.Add(acp.name,spt);
							break;
						}
					}

				}
				
			}

		}


			/*
			AnimatorControllerLayer acl= ac.GetLayer(0);
			if(acl!=null)
			{
				for(int i=0;i<acl.stateMachine.stateCount;i++)
				{
					State state= acl.stateMachine.GetState(i);
					if(state!=null)
					{
					  	Motion mot=state.GetMotion(acl);
						if(mot!=null)
						{
							AnimationClip acp= Game.EAtn(mot.name);
							if(acp!=null)
							{ 
								EditorCurveBinding[] ecb_ary=AnimationUtility.GetObjectReferenceCurveBindings(acp);
								for(int o=0;o<ecb_ary.Length;o++)
								{
									EditorCurveBinding ecb=ecb_ary[o];
									Debug.Log(ecb.ToString());
									//if();

								}
							}
						}
					}
				}


			}
		}*/


		//Game.ORM("EAtn","Prefab/EffObj/Animation/");
		//Game.ORM("EAtr","Prefab/EffObj/Animator/");


	}

	void OnDestroy()
	{
		//销毁临时特效物体
		GameObject.DestroyImmediate(effObj);
		effObj=null;
		effAni=null;
	}

	void OnProjectChange()
	{
		Repaint();
	}


	void OnSceneGUI () 
	{

	}

	void OnInspectorUpdate()
	{
		Repaint();
	}	

	void OnSelectionChange()
	{
		
	}

	void Update()
	{

	}

	void CheckSelection()
	{
		if(checkSelection)
		{
			if(timer>0f)
			{
				Selection.activeObject=selectionObj;
				checkSelection=false;
				timer=0;
			}
			else
			{
				timer+=Time.deltaTime;
			}
			
		}
	}

	//预选物体
	void Wilrnc_lstelection(GameObject go)
	{
		Selection.activeObject=null;
		selectionObj=go;
		checkSelection=true;
		timer=0;
	}

	void OnGUI()
	{
		CheckSelection();

		float d=20;
		float dh=d+1;
		float adx,ady;adx=ady=0;

		Vector2 spt_zoom=new Vector2(24,24);

		adx=0;
		if(GUI.Button(new Rect(adx,ady,150,d),"格式化动画时间始末"))
		{
			foreach(AnimationClip ac in ac_rnc_lstt)
			{
				EditorHelp.CheckAnimationClipEvent(ac);
			}
		}

		adx+=200;
		ady+=dh;
		adx=0;

		GUI.BeginGroup(new Rect(adx,ady,position.width,position.height));
		for(int i=0;i<s_rnc_lstt.Count;i++)
		{
			if(GUI.Button(new Rect(0,i*spt_zoom.y,spt_zoom.x,spt_zoom.y),""))
			{
				effAni.runtimeAnimatorController=Game.EAtr(s_rnc_lstt[i]);
				Wilrnc_lstelection(effAni.gameObject);
			}
			EditorHelp.EditorDrawSprite(new Rect(0,i*spt_zoom.y,spt_zoom.x,spt_zoom.y),spt_dic[s_rnc_lstt[i]]);
		}

		GUI.EndGroup();
	}
}
