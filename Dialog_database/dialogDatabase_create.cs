using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class dialogDatabase_create : MonoBehaviour {

	public string charaName;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Text t1 = new Text("Bonjour,");

			string a1 = "Bien";
			string a2 = "Pas bien";
			List<string> ans = new List<string> ();
			ans.Add (a1);
			ans.Add (a2);

			Text t2 = new Text ("Comment allez vous ?", ans);

			List<Text> tex = new List<Text> ();
			tex.Add (t1);
			tex.Add (t2);

			List<Condition> cond = new List<Condition> ();
			Condition c1 = new Condition ();
			c1.SetName("testCondition");
			c1.SetId(1);
			cond.Add (c1);

			Dialog dia = new Dialog (charaName,cond, tex);

			ConditionContainer.AddCondition ("Assets/Resources/Dialog_Database/condition.xml", c1);
			DialogContainer.AddDialog ("Assets/Resources/Dialog_Database/dialog.xml", dia);
		}
	}

}
