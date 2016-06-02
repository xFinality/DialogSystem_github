using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Editor_DialogWindow : EditorWindow {

	public Editor_ConditionWindow ecw;
	public Editor_TextWindow etw;

	public static ConditionContainer bddCondition;
	public const string conditionDatabasePath = "Assets/Resources/Dialog_database/condition.xml";

	private List<Condition> temp;

	private string welcomeMessage = "Bienvenue dans la création de Dialogue";
	private string characName = "chara name";

	private string conditionButton = "Add condition";
	private string textButton = "Add Text";

	//private string conditionButton = "Select Conditions";
	private List<Condition> conditions;
	private List<Text> texts;
	private int id;

	[MenuItem("Tools/Dialog System/Dialog_creation")]
	public static void DisplayWindow()
	{
		Editor_DialogWindow edw = (Editor_DialogWindow)EditorWindow.GetWindow (typeof(Editor_DialogWindow));
	}

	public void OnGUI()
	{
		GUILayout.Label (welcomeMessage);

		GUILayout.BeginHorizontal ("box");

			characName = EditorGUILayout.TextField ("Character Name :",characName);

		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ("box");

			GUILayout.Label ("Conditions :");
			if (conditions != null) {
				for (int i = 0; i < conditions.Count; i++) {
					EditorGUILayout.LabelField (conditions [i].GetName()+" : "+ conditions [i].GetValue().ToString());
				}
			}

		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ("box");

			GUILayout.Label ("Texts :");
			if (texts != null) {
				for (int i = 0; i < texts.Count; i++) {
					EditorGUILayout.LabelField (texts [i].text);
				}
			}
			
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ("box");

			if (GUILayout.Button (textButton)) {
				etw = (Editor_TextWindow)EditorWindow.GetWindow (typeof(Editor_TextWindow));
			}

			if (GUILayout.Button (conditionButton)) {
				ecw = (Editor_ConditionWindow)EditorWindow.GetWindow (typeof(Editor_ConditionWindow));
				ecw.SetDialogWindow (this);
			}

		GUILayout.EndHorizontal ();
	}

	void OnFocus()
	{
		if (ecw != null) {
			temp = ecw.GetBools ();
			for(int i =0; i<temp.Count;i++)
			{
				if (!temp [i].GetValue ()) {
					temp.Remove (temp [i]);
					i--;
				}
			}
			conditions = temp;
		}
	}

}