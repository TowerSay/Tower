using UnityEngine;
using UnityEditor;
using System.Collections;

public class ComponentXX : Editor 
{
	[MenuItem ("CONTEXT/Rigidbody/Do Something")]
	static void DoSomething1 (MenuCommand command) {
		Rigidbody body = (Rigidbody)command.context;
		body.mass = 5;
	}



	// Validate the menu item.
	//验证菜单项。
	// The item will be disabled if no transform is selected.
	//如果没有变换被选择，该项将是禁用的。
	[MenuItem ("GameObject/Do Something 2", true)]
	static bool ValidateDoSomething () {
		Debug.Log("到此为止ValidateDoSomething");
		return Selection.activeTransform != null;
	}

	[MenuItem ("Sinoi/定位相机",true)]
	static bool Init_1() 
	{
		Debug.Log("到此为止");
		return Selection.activeTransform != null;

	}
	/*
	[MenuItem ("Sinoi/组件/特殊相机",true)]
	static bool Init_2() 
	{
		return Selection.activeGameObject != null;
		
	}

*/
}
