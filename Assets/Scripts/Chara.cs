using UnityEngine;
using System.Collections;

public class Chara
{
	int _hp;
	public int max_hp;
	public int atk;
	public int def;
	public float hp_rate;
	public bool die=false;
	public int def_pen=0;
	public float def_rate_pen;

	public Chara(int hp,int atk,int def)
	{
		this.hp=this.max_hp=hp;
		this.atk=atk;
		this.def=def;
		
	}
	
	public int hp
	{
		get
		{
			return _hp;
		}
		
		set
		{
			_hp=value;
			if(_hp>max_hp)_hp=max_hp;
			hp_rate=(float)_hp/(float)max_hp;
			if(hp_rate<0)hp_rate=0;

			if(_hp<=0)
			{
				die=true;
			}
		}
	}
	
	
	public void Atk(Chara chr)
	{
		int real_def=chr.def;
		real_def-=Mathf.CeilToInt( def_rate_pen*real_def);
		real_def-=this.def_pen;
	

		if(this.atk<=real_def)
		{
			float rate=(float)this.atk/(float)(real_def+1);
			if(Random.Range(0f,1f)>rate)
			{
				chr.hp-=1;
			}
		}
		else chr.hp-=this.atk-real_def;
		
		
	}
}