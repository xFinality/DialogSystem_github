using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class Condition {

	[XmlAttribute("id")]
	public int id;

	[XmlAttribute("name")]
	public string name;

	[XmlElement("value")]
	public bool value;

	public Condition()
	{
		id = 0;
		name = "noCondition";
		value = false;
	}

	public Condition(int i, string n)
	{
		id = i;
		name = n;
	}

	public Condition(string n, bool v)
	{
		name = n;
		value = v;
	}

	public bool GetValue()
	{
		return value;
	}

	public void SetValue(bool b)
	{
		value = b;
	}

	public string GetName()
	{
		return name;
	}

	public void SetName(string n)
	{
		name = n;
	}

	public int GetId()
	{
		return id;
	}

	public void SetId(int i)
	{
		id = i;
	}

}

[System.Serializable]
[XmlRoot("ConditionCollection")]
public class ConditionContainer
{
	[XmlArray("Conditions")]
	[XmlArrayItem("Condition")]
	public List<Condition> conditions = new List<Condition>();

	public static ConditionContainer Load(string path)
	{
		//TextAsset _xml = Resources.Load<TextAsset> (path);

		XmlSerializer serializer = new XmlSerializer (typeof(ConditionContainer));

		FileStream reader = new FileStream (path, FileMode.Open);

		ConditionContainer conditions = serializer.Deserialize (reader) as ConditionContainer;
		reader.Close ();

		return conditions;
	}

	public static void Save(string path, ConditionContainer condC)
	{
		//TextAsset _xml = Resources.Load<TextAsset> (path);

		XmlSerializer serializer = new XmlSerializer (typeof(ConditionContainer));

		FileStream stream = new FileStream (path, FileMode.Create);
		serializer.Serialize (stream, condC);

		stream.Close ();

	}


	public static void AddCondition(string path, Condition cond)
	{
		ConditionContainer condC = GameControl.listCondition;
		//DialogContainer diaC = Load (path);
		condC.conditions.Add (cond);
		Save (path, condC);
	}
}