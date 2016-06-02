using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DialogController : MonoBehaviour {

	public static DialogController control;

	public static string itemDatabasePath = "Assets/Resources/Database/items.xml";
	public static string dialogDatabasePath = "Assets/Resources/Dialog_database/dialog.xml";
	public static string conditionDatabasePath = "Assets/Resources/Dialog_database/condition.xml";

	public static DialogContainer listDialog;

	public static ConditionContainer listCondition;

	void Awake()
	{
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;

			listDialog = DialogContainer.Load (dialogDatabasePath);
			listCondition = ConditionContainer.Load (conditionDatabasePath);
			DialogContainer.SetCondition (listDialog);

		} else if (control != this) {
			Destroy(gameObject);
		}
	}

}
