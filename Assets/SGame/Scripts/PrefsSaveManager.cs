using UnityEngine;
using System.Collections;
using System.IO;

public class PrefsSaveManager 
{
	string name;
	string path;
	public PrefsSaveManager(string name)
	{
		this.name=name;
		path=Game.DataPath("PrefSave/"+name);


		/*
#if UNITY_ANDROID
		string filepath = Application.dataPath + "!/assets/" + path;
		if(!File.Exists(filepath))
		{
			path=filepath;
		}

#endif

		if(Application.platform==RuntimePlatform.Android)
		{
			filepath = Application.persistentDataPath + "/" + path;
		}
		else if(Application.platform==RuntimePlatform.IPhonePlayer)
		{
			filepath = Application.dataPath  + "/Raw/" + path;
		}
		else
		{
			filepath = Application.dataPath + "/StreamingAssets/" + path;
		}

		if(!File.Exists(path))
		{
			File.Create(path);
		}

		//Game.CheckCopyAndroidPath(path);
		*/
	}

	public void Set(string key,string value)
	{
	//	FileStream fs=File.Open("",FileMode.OpenOrCreate);
	//	BinaryWriter bw=new BinaryWriter(fs);
	//	int ptr=bw.Seek(0,SeekOrigin.Begin);
	//	bw.Write();



	}

}
