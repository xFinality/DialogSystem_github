using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Editor_TextWindow : EditorWindow {

	[MenuItem("Tools/Dialog System/Text_creation")]
	public static void DisplayWindow()
	{
		Editor_TextWindow edw = (Editor_TextWindow)EditorWindow.GetWindow (typeof(Editor_TextWindow));
	}


	public void OnGUI()
	{

	}
}
