using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/*
[CanEditMultipleObjects,CustomEditor(typeof(CharaInfo))]

public class CharaInfoEditor :EditorBase<CharaInfo>
{
	SerializedProperty armor;
	SerializedProperty damage;
	SerializedProperty curve;
	private SerializedObject chara;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnEnable ()
	{
		chara = new SerializedObject(target);
		damage = chara.FindProperty ("damage");
		armor = chara.FindProperty ("armor");
		curve = chara.FindProperty ("curve");
	}

	public override void OnInspectorGUI() 
	{
		chara.Update();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("角色基本信息:");
		EditorGUILayout.EndHorizontal();
	//	EditorGUILayout.PropertyField(damage);
		// Show the custom GUI contrornc_lst
	//	EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginHorizontal();
		damage.intValue = EditorGUILayout.IntSlider  ("伤害:",damage.intValue, 0, 100);
		//if (!damage.hasMultipleDifferentValues)
	//		EditorGUI.ProgressBar (GUILayoutUtility.GetRect(20,20),damage.intValue / 100.0f, "伤害");
	//	EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		armor.intValue= EditorGUILayout.IntSlider ("护甲", armor.intValue, 0, 100);
		// Only show the armor progress bar if all the objects have the same armor value:
	//	if (!armor.hasMultipleDifferentValues)
	//		EditorGUI.ProgressBar (GUILayoutUtility.GetRect(20,20),armor.intValue / 100.0f, "护甲");
		EditorGUILayout.EndHorizontal();

		curve.animationCurveValue=EditorGUILayout.CurveField("曲线",curve.animationCurveValue);

		chara.ApplyModifiedProperties();
	}
}*/
