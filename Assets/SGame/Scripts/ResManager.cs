using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
v 1.0
1 支持所有Object子类型资源的存读
2 支持同名不同类型的资源的存读
*/

/// <summary>
///资源管理器
/// </summary>
public class ResManager
{
	List<Object> obj_rnc_lstt=new List<Object>();
	Dictionary<string,Object> objs=new Dictionary<string,Object>();

	/// <summary>路径</summary>
	string path;
	bool resourcesLoad=true;

	public ResManager(string path,bool resources_load=true)
	{
		this.path=path;
		this.resourcesLoad=resources_load;
		Debug.Log(path);
	}

	/// <summary>
	/// 获取某类型资源
	/// </summary>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T Res<T>(string name)where T : Object
	{
		System.Type type= typeof(T);
		string key=name+type.Name;

		if(!objs.ContainsKey(key))
		{
			if(resourcesLoad)objs[key]=Resources.Load(path+name,type);
			else objs[key]=Resources.LoadAssetAtPath(path+name,type);

			obj_rnc_lstt.Add(objs[key]);
		}
		return objs[key] as T;
	}

	/// <summary>得到某个精灵</summary> 
	public Sprite Spt(string name){return Res<Sprite>(name);}

	/// <summary>得到某个音频剪辑</summary> 
	public AudioClip Audio(string name){return Res<AudioClip>(name);}

	/// <summary>得到某个预设物体</summary> 
	public GameObject Obj(string name){return Res<GameObject>(name);}

	/// <summary>
	/// 清空所有资源
	/// </summary>
	public void ClearAllObj()
	{
		for(int i=0;i<obj_rnc_lstt.Count;i++)
		{
			objs.Remove(obj_rnc_lstt[i].name);
			Resources.UnloadAsset(obj_rnc_lstt[i]);
		}
		Resources.UnloadUnusedAssets();
	}
}

