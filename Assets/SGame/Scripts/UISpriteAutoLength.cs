using UnityEngine;
using System.Collections;

[AddComponentMenu("游戏/NGUI附加/UISprite调节条")]
[ExecuteInEditMode]
public class UISpriteAutoLength : MonoBehaviour 
{
	public UISpriteAutoLength bind;

	public float rate;
	public float value;
	public float localScaleX;
	
	UISprite _spt;

	 float last_value=-1;
	float last_rate=-1;

	void Awake()
	{
#if UNITY_EDITOR && DEBUG
		localScaleX=this.transform.localScale.x;
		value=spt.width;
#endif
	}

	void Update ()
	{
		if(bind!=null)
		{
			value=bind.value;

			if(rate!=bind.rate)
			{
				float scale=(bind.rate-rate)*3f;
				rate+=scale*Game.RealDeltaTime();
			}
		}


		
		bool renew=false;
		if(last_value!=value)
		{
			last_value=value;
			renew=true;
		}
		
		if(last_rate!=rate)
		{
			rate=Mathf.Clamp(rate,0f,1f);

			last_rate=rate;
			renew=true;
		}
			
			if(renew)
			{
				Vector3 v3=this.transform.localScale;
				float width=value*rate;
				if(width<spt.minWidth)
				{
					float zoom= (width/(float)spt.minWidth);
					
					spt.width=spt.minWidth;
					v3.x=localScaleX*zoom;
					
				}
				else {spt.width=(int)(value*rate);v3.x=localScaleX;}
				this.transform.localScale=v3;
			}
		




	}

	public UISprite spt
	{
		get
		{
			if(_spt==null)
			{
				_spt=GetComponent<UISprite>();
			}
			return  _spt;
		}
	}
}
