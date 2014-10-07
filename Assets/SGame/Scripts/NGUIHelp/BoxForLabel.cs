using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BoxForLabel : MonoBehaviour 
{
	public UILabel _lb;

	public enum TYPE
	{
		BOX_2D,
		BOX_3D
	}
	public TYPE type=TYPE.BOX_2D;

	BoxCollider2D _bc2;
	BoxCollider _bc;

	TYPE last_type; 

	void GetBox()
	{
		if(last_type==TYPE.BOX_2D)
		{
			if(_bc2!=null)DestroyImmediate(_bc2);
			_bc=GetComponent<BoxCollider>();
			if(_bc==null)
			{
				_bc=gameObject.AddComponent<BoxCollider>();
			}
		}
		else if(last_type==TYPE.BOX_3D)
		{
			if(_bc!=null)DestroyImmediate(_bc);
			_bc2=GetComponent<BoxCollider2D>();
			if(_bc2==null)
			{
				_bc2=gameObject.AddComponent<BoxCollider2D>();
			}
		}
	}


	void Enable()
	{
#if UNITY_EDITOR 
		last_type=type;
		GetBox();
		RenewWH();
#endif
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
#if UNITY_EDITOR 
		if(last_type!=type)
		{
			GetBox();
			//RenewWH();
			last_type=type;
		}
		else RenewWH();
#endif
	}

	
	void RenewWH()
	{
		if(type==TYPE.BOX_2D)
		{
			bc2.size=new Vector2( lb.width,lb.height);
		}
		else if(type==TYPE.BOX_3D)
		{
			bc.size=new Vector3(lb.width,lb.height,bc.size.z);
		}
	}

	public BoxCollider2D bc2
	{
		get
		{
			if(_bc2==null)
			{
				_bc2=GetComponent<BoxCollider2D>();
				if(_bc2==null)
				{
					_bc2=gameObject.AddComponent<BoxCollider2D>();
				}
			}
			return _bc2;
		}
	}

	public BoxCollider bc
	{
		get
		{
			if(_bc==null)
			{
				_bc=GetComponent<BoxCollider>();
				if(_bc==null)
				{
					_bc=gameObject.AddComponent<BoxCollider>();
				}
			}
			return _bc;
		}
	}

	public UILabel lb
	{
		get
		{
			if(_lb==null)
			{
				_lb=GetComponent<UILabel>();
				if(_lb==null)
				{
					_lb=gameObject.AddComponent<UILabel>();
				}
			}
			return _lb;
		}
	}
}
