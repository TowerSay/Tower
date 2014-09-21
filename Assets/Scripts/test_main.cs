using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test_main : MonoBehaviour 
{
	public Texture t;
	public Texture bg;
	float timer;

	bool update=true;

	Chara a;
	Chara b;
	List<Chara> chrs=new List<Chara>();
	//List<Chara> team1=new List<Chara>();

	//你是这么觉得的吗
	// Use this for initialization
	void Start () 
	{
		//chrs.Add(NetworkReachability );
		a=new Chara(105,20,1);
		a.def_rate_pen=0.4f;

		b=new Chara(105,20,5);	
		chrs.Add(a);
		chrs.Add(b);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(update)
		{
			if(timer>0.5f)
			{
				timer=0;
				a.Atk(b);
				b.Atk(a);

				if(a.die || b.die)
				{
					update=false;
				}

			}else timer+=Time.deltaTime;
		}


	}
	
	void OnGUI()
	{
		float dx,dy;dx=dy=4;
		int d=22;
		for(int i=0;i<chrs.Count;i++)
		{

			Chara chr = chrs[i];
			GUI.Box(new Rect(dx,dy,200,d),"hp:"+chr.hp+"/"+chr.max_hp);dy+=d;
			GUI.Box(new Rect(dx,dy,200,d),"atk:"+chr.atk);dy+=d;
			GUI.Box(new Rect(dx,dy,200,d),"def:"+chr.def);dy+=d;
			GUI.Box(new Rect(dx,dy,200,d),"rate:"+(chr.hp_rate*100).ToString("f1")+"%");dy+=d;
			GUI.DrawTexture(new Rect(dx,dy,200,d),bg);
			GUI.DrawTexture(new Rect(dx,dy,200*chr.hp_rate,d),t);

			dy=4;
			dx+=200+d;
		}
	}
}
