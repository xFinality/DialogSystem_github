using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

	public PlayerData data;

	public List<Dialog> dialog;

	//gerer ca autrement
	public GameObject dialogPanel;
	public GameObject answerPanel;
	public GameObject continuButton;

	private bool continu;

	void Start()
	{
		bool check = false;
		continu = false;
		dialog = new List<Dialog> ();
		foreach(Dialog dia in GameControl.listDialog.dialogs)
		{
			if (dia.GetCharactersName() == this.name) {
				foreach(Dialog d in dialog) {
					if (dia.GetId() == d.GetId()) {
						check = true;
					}
				}
				if(check == false)
					dialog.Add (dia);
			}
			check = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		StartCoroutine ("DialogCoroutine");
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			StopCoroutine ("DialogCoroutine");

			GameObject[] ui = GameObject.FindGameObjectsWithTag ("UI");
			for (int i=0; i< ui.Length; i++) {
				Destroy(ui[i]);
			}

			dialogPanel.SetActive (false);
			answerPanel.SetActive (false);

		}
	}

	IEnumerator DialogCoroutine()
	{
		continuButton.SetActive(true);
		for (int i = 0; i < dialog.Count; i++) {
			if (dialog [i].CheckDialogCondition ()) {
				dialogPanel.SetActive (true);
				foreach (Text t in dialog[i].GetText()) {
					dialogPanel.transform.GetComponentInChildren<UnityEngine.UI.Text> ().text = t.GetText();

					int n = 10;
					int x = 30;

					if (t.GetAnswer().Count > 0) {

						continuButton.SetActive (false);

						for (int j = 0; j < t.GetAnswer().Count; j++) {

							GameObject button = (GameObject)GameObject.Instantiate (Resources.Load<GameObject> ("UI/Button"));

							button.transform.SetParent (answerPanel.transform, false);

							button.GetComponent<RectTransform> ().pivot = new Vector2 (0, 1);
							button.GetComponent<RectTransform> ().anchorMin = new Vector2 (0, 1);
							button.GetComponent<RectTransform> ().anchorMax = new Vector2 (0, 1);



							button.GetComponent<RectTransform> ().Translate (new Vector3 (x, -n, 0));

							button.GetComponent<RectTransform> ().sizeDelta = new Vector2 (160, 30);

							button.GetComponent<Button> ().onClick.AddListener (() => OnContinu ());

							button.GetComponentInChildren<UnityEngine.UI.Text> ().text = t.GetAnswer() [j];
							button.name = "AnswerButton" + j;
							button.tag = "UI";

							x += 170;
						}
						answerPanel.SetActive (true);
						yield return new WaitUntil (() => continu == true);
						continu = false;

					} else {
						continuButton.SetActive (true);
						yield return new WaitUntil (() => continu == true);
						continu = false;
					}
					answerPanel.SetActive (false);
				}
			}
			dialogPanel.SetActive (false);
		}
	}

	public void OnContinu(){	

		GameObject[] ui = GameObject.FindGameObjectsWithTag ("UI");
		for (int i=0; i< ui.Length; i++) {
			Destroy(ui[i]);
		}
		continu = true;
	}
}
