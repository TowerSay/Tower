using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharaBaseInfo:ICloneable
{
	float _HP;
	float _MaxHP;
	float _HPRate;

	public float Atk;
	public float Def;

	public CharaBaseInfo()
	{

	}

	public float HP
	{
		get
		{
			return _HP;
		}
		set
		{
			_HP=value;
			_HP=Mathf.Clamp(_HP,0,_MaxHP);

			RenewHPRate();
		}
	}

	public float MaxHP
	{
		get
		{
			return _MaxHP;
		}
		set
		{
			_MaxHP=value;
			if(_MaxHP<0)
			{
				_MaxHP=0;
			}

			RenewHPRate();
		}
	}

	void RenewHPRate()
	{
		_HPRate=_HP/_MaxHP;
	}

	public float HPRate
	{
		get
		{
			return _HPRate;
		}
	}


	public object Clone()
	{
		return this.MemberwiseClone();
	}

	public CharaBaseInfo IClone()
	{
		return (CharaBaseInfo)(Clone());
	}


}
