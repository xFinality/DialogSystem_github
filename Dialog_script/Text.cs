using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class Text {

	//Text		to display
	[XmlElement("text")]
	public string text;

	//Answer	possible to this text
	[XmlArray("Answers")]
	[XmlArrayItem("Answer")]
	public List<string> answer;

	public Text()
	{
		text = "noText";
		answer = new List<string> ();
	}

	public Text(string t)
	{
		text = t;
	}

	public Text(string t, List<string> a)
	{
		text = t;
		answer = a;
	}

	public string GetText()
	{
		return text;
	}

	public List<string> GetAnswer()
	{
		return answer;
	}

}

[System.Serializable]
public struct Answer
{
	[XmlElement("answer")]
	public string answer;
	public Condition cond;
}
