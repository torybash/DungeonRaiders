using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public static class Menus {


	[MenuItem("Run/Run from MainMenu %#r")]
	private static void RunFromMenu()
	{
		if ( EditorApplication.isPlaying == true )
		{
			EditorApplication.isPlaying = false;
			return;
		}
		EditorApplication.SaveCurrentSceneIfUserWantsTo();
		EditorApplication.OpenScene("Assets/MainMenu.unity");
		EditorApplication.isPlaying = true;
	}
}
