using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKeyScript : MonoBehaviour {

	[SerializeField] Transform buttons;
	Event keyEvent;
	KeyCode newKey;
	bool waitingForKey = false;

	// Use this for initialization
	void Start () {
		buttons.Find("Button-Left").Find("Key").GetComponent<Text>().text = GameInputManager.instance.left.ToString();
		buttons.Find("Button-Right").Find("Key").GetComponent<Text>().text = GameInputManager.instance.right.ToString();
		buttons.Find("Button-Jump").Find("Key").GetComponent<Text>().text = GameInputManager.instance.jump.ToString();
	}

	void OnGUI() {
		keyEvent = Event.current;
		if(keyEvent.isKey && waitingForKey) {
			newKey = keyEvent.keyCode;
			waitingForKey = false;
		}
	}

	public void StartAssignment(string keyName) {
		if (!waitingForKey) StartCoroutine(AssignKey(keyName));
	}

	IEnumerator WaitForKey() {
		while (!keyEvent.isKey) yield return null;
		yield return new WaitForSeconds(2);
	}

	IEnumerator DotAnmation(string keyName) {
		Text textComponent = buttons.Find("Button-" + keyName.Capitalize()).Find("Key").GetComponent<Text>();
		while (waitingForKey) {
			for(int i=0; i<3; i++) {
				textComponent.text = ".".RepeatForLoop(i + 1);
				yield return new WaitForSeconds(0.3f);
			}
		}
	}

	public IEnumerator AssignKey(string keyName) {
		waitingForKey = true;
		StartCoroutine(DotAnmation(keyName));

		yield return WaitForKey();

		string buttonString = "";
		keyName = keyName.ToLower();

		if (keyName == "left") {
			GameInputManager.instance.left = newKey;
			buttonString = GameInputManager.instance.left.ToString();
		}
		else if (keyName == "right") {
			GameInputManager.instance.left = newKey;
			buttonString = GameInputManager.instance.left.ToString();
		}
		else if (keyName == "jump") {
			GameInputManager.instance.left = newKey;
			buttonString = GameInputManager.instance.left.ToString();
		}

		buttons.Find("Button-" + keyName.Capitalize()).Find("Key").GetComponent<Text>().text = buttonString;
		PlayerPrefs.SetString(keyName + "Key", buttonString);

		yield return null;
	}
}
