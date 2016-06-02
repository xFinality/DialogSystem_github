using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class test : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			for (int i = 0; i < GameControl.listCondition.conditions.Count; i++) {
				GameControl.listCondition.conditions [i].SetValue (true);
			}
		}

	}
}
