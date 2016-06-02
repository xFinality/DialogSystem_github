using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Editor_ConditionWindow : EditorWindow {

	public static ConditionContainer bddCondition;
	public const string conditionDatabasePath = "Assets/Resources/Dialog_database/condition.xml";
	public Editor_DialogWindow edw;

	private List<Condition> bools;
	private bool[] temp;

	private string validationButton = "Valider";

	[MenuItem("Tools/Dialog System/Condition_creation")]
	public static void DisplayWindow()
	{
		Editor_ConditionWindow edw = (Editor_ConditionWindow)EditorWindow.GetWindow (typeof(Editor_ConditionWindow));
	}

	public void Awake()
	{
		bddCondition = ConditionContainer.Load (conditionDatabasePath);
		bools = new List<Condition> ();
		temp = new bool[bddCondition.conditions.Count];
	}

	public void OnGUI()
	{
		if (bddCondition != null) {
			for (int i = 0; i < bddCondition.conditions.Count; i++) {
				temp [i] = EditorGUILayout.Toggle (bddCondition.conditions [i].GetName (), temp [i]);
			}
		}

		if (GUILayout.Button (validationButton)) {
			edw.Focus ();
			this.Close ();
		}
	}

	public List<Condition> GetBools()
	{
		bools.Clear();
		for (int i = 0; i < bddCondition.conditions.Count; i++) {
			Condition c = new Condition (bddCondition.conditions [i].GetName (), temp[i]);
			bools.Add (c);
		}

		Debug.Log (bools.Count);
		return bools;
	}

	public void SetDialogWindow(Editor_DialogWindow ed)
	{
		edw = ed;
	}

}
