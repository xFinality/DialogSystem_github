using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class Dialog {

	//Character name 	of the character that say the dialog
	[XmlAttribute("character")]
	public string characters_dialog;

	//Conditions	to run this dialog
	[XmlArray("Conditions")]
	[XmlArrayItem("Condition")]
	public List<Condition> conditions;

	//Text			of the dialog
	[XmlArray("Texts")]
	[XmlArrayItem("Text")]
	public List<Text> texts;

	[XmlAttribute("id")]
	public int id;

	public Dialog()
	{
		id = 0;
		characters_dialog = "noName";
		conditions = new List<Condition> ();
		texts = new List<Text> ();
	}

	public Dialog(string chara)
	{
		id = 0;
		characters_dialog = chara;
		conditions = new List<Condition> ();
		texts = new List<Text> ();
	}

	public Dialog(string chara, List<Text> tex)
	{
		id = 0;
		characters_dialog = chara;
		texts = tex;
		conditions = new List<Condition> ();
	}

	public Dialog (string chara, List<Condition> cond, List<Text> tex)
	{
		id = 0;
		characters_dialog = chara;
		conditions = cond;
		texts = tex;
	}

	public Dialog (string chara, List<Condition> cond, List<Text> tex, int i)
	{
		id = i;
		characters_dialog = chara;
		conditions = cond;
		texts = tex;
	}

	public int GetId()
	{
		return id;
	}

	public string GetCharactersName()
	{
		return characters_dialog;
	}

	public List<Text> GetText()
	{
		return texts;
	}

	public List<Condition> GetCondition()
	{
		return conditions;
	}

	public bool CheckDialogCondition()
	{
		foreach (Condition c in conditions) {
			if (c != null) {
				if (!c.GetValue ()) {
					return false;
				}
			}
		}
		return true;
	}


}

[System.Serializable]
[XmlRoot("DialogCollection")]
public class DialogContainer{
	[XmlArray("Dialogs")]
	[XmlArrayItem("Dialog")]
	public List<Dialog> dialogs = new List<Dialog>();

	public static DialogContainer Load(string path)
	{
		//TextAsset _xml = Resources.Load<TextAsset> (path);

		XmlSerializer serializer = new XmlSerializer (typeof(DialogContainer));

		FileStream reader = new FileStream (path, FileMode.Open);

		DialogContainer dialogs = serializer.Deserialize (reader) as DialogContainer;
		reader.Close ();

		return dialogs;
	}

	public static void Save(string path, DialogContainer diaC)
	{
		//TextAsset _xml = Resources.Load<TextAsset> (path);

		XmlSerializer serializer = new XmlSerializer (typeof(DialogContainer));

		FileStream stream = new FileStream (path, FileMode.Create);
		serializer.Serialize (stream, diaC);

		stream.Close ();

	}


	public static void AddDialog(string path, Dialog dia)
	{
		DialogContainer diaC = GameControl.listDialog;
		//DialogContainer diaC = Load (path);
		diaC.dialogs.Add (dia);
		Save (path, diaC);
	}

	public static void SetCondition(DialogContainer diaC)
	{
		foreach (Dialog dia in diaC.dialogs) {
			for(int i=0; i<dia.conditions.Count;i++)
			{
				dia.conditions[i] = GameControl.listCondition.conditions.Find (c => c == dia.conditions[i]);
			}
		}
	}

	public static void SetCondition(DialogContainer diaC, Condition cond)
	{
		foreach (Dialog dia in diaC.dialogs) {
			for (int i = 0; i < dia.conditions.Count; i++) {
				if (dia.conditions [i].GetId () == cond.GetId ()) {
					dia.conditions [i] = GameControl.listCondition.conditions.Find (c => c == cond);
				}
			}
		}
	}

}