using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*命名规则:

1 通常自定义类型名称：关键字首字母大写, 后续小写
	比如: ResManager   PrefsSaveManager   SpriteEditor
	
2 通常类型对应的实例名称 ：关键字首字母小写, 后续规则如1
	比如: ResManager -> resManager 
	比如: SpriteRenderer -> spriteRenderer 
	
3 特别：具备强区分价值的使用下划线表示： 名称缩写(小写)_类型缩写(小写)  
	比如: res_rnc_lst -> 资源_链表
	比如: spt_ary -> 精灵_数组
	比如: obj_dic -> 物体_字典
*/

public static class Game
{
	private static Dictionary<string,ResManager> resManager=new Dictionary<string, ResManager>();
	private static Dictionary<string,PrefsSaveManager> prefsManager=new Dictionary<string, PrefsSaveManager>();

	/// <summary>供编辑器使用_资源管理器</summary>
	public static ResManager ERM=new ResManager("Assets/SGame/Editor/SysRes/",false);

	/// <summary>游戏后台实时管理器</summary>
	public static RealTimeManager RTM;

	/// <summary>场景后台实时管理器</summary>
	private static SRTMBase _SRTM;

	/// <summary>取场景后台实时管理器</summary>
	public static T SRTM<T>()where T : SRTMBase
	{
		if(_SRTM!=null && _SRTM.GetType()==typeof(T))
		{
			return (T)_SRTM;
		}
		else {_SRTM=GameObject.FindObjectOfType(typeof(T)) as SRTMBase;}

		return (T)_SRTM;
	}

	/// <summary>获取某个资源管理器</summary>
	public static ResManager ORM(string name,string basePath="Prefab")
	{
		if(!resManager.ContainsKey(name))
		{
			resManager.Add(name,new ResManager(basePath+"/"));
		}
		return resManager[name];
	}

	/// <summary>获取设置信息存储管理器</summary>
	public static PrefsSaveManager PSM(string name)
	{
		if(!resManager.ContainsKey(name))
		{
			prefsManager.Add(name,new PrefsSaveManager(name));
		}
		return prefsManager[name];
	}

	//以下通常管理器资源的快速取得方法---------------------------------------------

	/// <summary> 取角色预设</summary>
	public static GameObject CObj(string name){return ORM("ChrObj","Prefab/ChrObj").Obj(name);}

	/// <summary> 取效果物体预设</summary>
	public static GameObject EObj(string name){return ORM("EffObj","Prefab/EffObj").Obj(name);}

	/// <summary> 取效果物体动画剪辑</summary>
	public static AnimationClip EAtn(string name){return ORM("EAtn","Prefab/EffObj/Animation").Res<AnimationClip>(name);}

	/// <summary> 取效果物体动画控制器</summary>
	public static RuntimeAnimatorController EAtr(string name){return ORM("EAtr","Prefab/EffObj/Animator").Res<RuntimeAnimatorController>(name);}

	/// <summary> 取系统物体预设</summary>
	public static GameObject SObj(string name){return ORM("SysObj","Prefab/SysObj").Obj(name);}

	/// <summary> 取向导物体预设</summary>
	public static GameObject GObj(string name){return ORM("GdeObj","Prefab/GdeObj").Obj(name);}

	/// <summary> 取资源物体预设</summary>
	public static GameObject RObj(string name){return ORM("ResObj","Prefab/ResObj").Obj(name);}
	
	/// <summary> 取投射物预设</summary>
	public static GameObject AObj(string name){return ORM("ArwObj","Prefab/ArwObj").Obj(name);}

	/// <summary> 取音频剪辑</summary>
	public static AudioClip Snd(string name){return ORM("Snd","Audio/Snd").Audio(name);}

	/// <summary> 取音频剪辑</summary>
	public static AudioClip Bgm(string name){return ORM("Bgm","Audio/Bgm").Audio(name);}

	/// <summary> 取精灵</summary>
	public static Sprite Spt(string name){return ORM("Spt","Sprite").Spt(name);}

	/// <summary> 取纹理</summary>
	public static Texture2D Tex(string name){return ORM("Tex","Texture").Res<Texture2D>(name);}


	//以下常用在场景中的方法---------------------------------------------

	/// <summary> 播放音效</summary>
	public static void PlaySound(string name,float volume=1f,float picth=1f)
	{
		NGUITools.PlaySound(Snd(name),volume,picth);
	}

	/// <summary> 切换背景音乐</summary>
	public static void SwapBGM(string name,float volume=1f,float picth=1f)
	{
		RTM.audio_source.clip=Bgm(name);
		RTM.audio_source.Play();
	}

	/// <summary> 检查安卓path文件，进行拷贝</summary>
	public static void CheckCopyAndroidPath(string path)
	{
		if(Application.platform==RuntimePlatform.Android)
		{
			string filepath = Application.persistentDataPath + "/" + path;
			if(!File.Exists(filepath))
			{
				/*Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
				                 Application.dataPath + "!/assets/" + path);*/
				WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + path);
				while(!loadDB.isDone) {}
				File.WriteAllBytes(filepath, loadDB.bytes);
			}
		}
	}

		/// <summary> 取个人数据路径</summary>
	public static string DataPath(string path,bool copyAndroid=false)
	{
		string filepath="";
		if(Application.platform==RuntimePlatform.Android)
		{
			filepath = Application.persistentDataPath + "/" + path;
			if(copyAndroid)CheckCopyAndroidPath(path);
		}
		else if(Application.platform==RuntimePlatform.IPhonePlayer)
		{
			filepath = Application.dataPath  + "/Raw/" + path;
		}
		else
		{
			filepath = Application.dataPath + "/StreamingAssets/" + path;
		}

		return filepath;
	}

	//世界时间缩放机制-----------------------------------------------------------

	/// <summary> 世界时间缩放值</summary>
	public static float worldTimeScale=1f;

	/// <summary> 取真实帧周期</summary>
	public static float RealDeltaTime(bool timeScale=false)
	{
		if(timeScale)return Time.deltaTime*worldTimeScale;
		else return Time.deltaTime;
	}

	/// <summary> 世界时间参照缩放</summary>
	public static float WorldScale(float value){return value*worldTimeScale;}


	//资源处理-----------------------------------------------------------

	private static Dictionary<string,SpriteClipPack> dic_scp=new Dictionary<string, SpriteClipPack>();

	/// <summary> 添加一个精灵包</summary>
	public static void CreateSpriteClipPack(string pack_name,Point size)
	{
		if(!dic_scp.ContainsKey(pack_name))
		{
			dic_scp.Add(pack_name,new SpriteClipPack(pack_name,size));
		}
	}

	public static SpriteClipPack GetSpriteClipPack(string pack_name)
	{
		return dic_scp[pack_name];
	}

	/// <summary> 在精灵包中取精灵</summary>
	public static Sprite Spt(string pack_name,Point id)
	{
		if(!dic_scp.ContainsKey(pack_name))
		{
			Debug.Log("不存在该精灵包!请先用CreateSpriteClipPack函数添加该包");
			return null;
		}
		return dic_scp[pack_name].spts[id];
	}


	/// <summary>按大小裁剪精灵包,由Point进行索引 最小下标(1,1) </summary>
	public static Dictionary<Point,Sprite> ClipFromMainTexture(Texture2D tex,Point size)
	{
		Dictionary<Point,Sprite> dic_ps=new Dictionary<Point, Sprite>();
		int w_size=tex.width/size.x;
		int h_size=tex.height/size.y;

		//Transform root=GameObject.Find("UI Root").transform;

		for(int i=0;i<w_size;i++)
		{
			for(int o=0;o<h_size;o++)
			{
				Rect rect=new Rect(size.x*i,size.y*(h_size-o-1),size.x,size.y);
				Sprite _spt=Sprite.Create(tex,rect,new Vector2(0.5f,0.5f));
				dic_ps.Add(new Point(i+1,o+1),_spt);

				/*
				GameObject go=GameObject.Instantiate(Game.RObj("2DSPT")) as GameObject;
				UI2DSprite spt=go.GetComponent<UI2DSprite>();
				spt.sprite2D=_spt;
				go.transform.parent=root;
				go.transform.localScale=Vector3.one;
				spt.width=size.x;
				spt.height=size.y;
				go.transform.localPosition=new Vector3(i*size.x,o*size.y,0);*/
			}
		}

		return dic_ps;
	}

	public static T Instantiate<T>(Object obj)where T : Component
	{
		GameObject go=GameObject.Instantiate(obj) as GameObject;
		return go.GetComponent(typeof(T)) as T;
	}


}




/// <summary>游戏帮助类(提供额外的常用接口)</summary>
public static class GameHelp
{
	/// <summary>取随机整数</summary>
	static public int Random (int min, int max)
	{
		if (min == max) return min;
		return UnityEngine.Random.Range(min, max + 1);
	}



	/// <summary>取随机ture/false</summary>
	static public bool Random ()
	{
		return (UnityEngine.Random.Range(0, 2)==1);
	}

	/// <summary>取随机小数</summary>
	static public float Random (float min, float max)
	{
		if (min == max) return min;
		return UnityEngine.Random.Range(min, max);
	}

	/// <summary> 链表转数组</summary>
	public static T[] ListToArray<T>(List<T> list)
	{
		T[] array=new T[list.Count];
		for(int i=0;i<list.Count;i++)
		{
			array[i]=list[i];
		}
		return array;
	}
	

}

