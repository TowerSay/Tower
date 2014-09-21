using UnityEngine;
using System.Collections;

public class rnc_lst_xxx : MonoBehaviour {

	public Sprite spt;
	// Use this for initialization
	void Start () {
	



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EditorGUI_DrawSprite(Rect rect,Sprite spt)
	{
	
		Vector2 offset= new Vector2( spt.rect.width/(float)spt.texture.width,spt.rect.height/(float)spt.texture.height);
		Vector2 pos=new Vector2( spt.rect.x/(float)spt.texture.width,spt.rect.y/(float)spt.texture.height);
		Rect _rect=new Rect(pos.x,pos.y,offset.x,offset.y);

		GUI.DrawTextureWithTexCoords(rect,spt.texture,_rect);
		//EditorGUI.DrawTextureAlpha(rect,spt.texture);
	}

	void OnGUI()
	{
		EditorGUI_DrawSprite(new Rect(20,200,64,64),spt);
	}
}
