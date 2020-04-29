using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKeyScript : MonoBehaviour {

	[SerializeField] Transform buttons;
	Event keyEvent;
	KeyCode newKey;
	bool waitingForKey = false;

	void Start () {
		// Get the current keyButton from GameInputManager instance and save in UI-Text components.
		buttons.Find("Button-Left").Find("Key").GetComponent<Text>().text = GameInputManager.instance.left.ToString();
		buttons.Find("Button-Right").Find("Key").GetComponent<Text>().text = GameInputManager.instance.right.ToString();
		buttons.Find("Button-Jump").Find("Key").GetComponent<Text>().text = GameInputManager.instance.jump.ToString();
	}

	/*
	 * OnGUI is called for rendering and handling GUI events.
	 * This means that your OnGUI implementation might be called several times per frame (one call per event).
	 * https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnGUI.html
	 * The current keyEvent and save in variable. If it's triggered by keyButton and the script is waiting for newInput (mutex variable)
	 * Save the value in newKey and set to false waitingForKey
	 */
	void OnGUI() {
		keyEvent = Event.current;
		if(keyEvent.isKey && waitingForKey) {
			newKey = keyEvent.keyCode;
			waitingForKey = false;
		}
	}

	// Method triggered by UI, just for start the coorutine AssignKey
	public void StartAssignment(string keyName) {
		if (!waitingForKey) StartCoroutine(AssignKey(keyName));
	}

	IEnumerator WaitForKey() {
		while (!keyEvent.isKey) yield return null;
		yield return new WaitForSeconds(0.3f);
	}

	// A simple text animation, after start AssignKey the UI is waiting for a newInput, show animated-dot . .. ...
	IEnumerator DotAnimation(string keyName) {
		Text textComponent = buttons.Find("Button-" + keyName.Capitalize()).Find("Key").GetComponent<Text>();
		while (waitingForKey) {
			for(int i=0; i<3; i++) {
				textComponent.text = ".".RepeatForLoop(i + 1);
				if (!waitingForKey) break;
				else yield return new WaitForSeconds(0.3f);
			}
		}
	}

	/*
	 * Set mutex variable to true
	 * Start coorutine for DotAnimation
	 * Wait yiel from WaitForKey (the keyEvent is a button!)
	 * Easy switch case / with if. Save the newKey value inside GameInputManager instance and get a string value
	 * Update the UI , save PlayerPrefs
	 */
	public IEnumerator AssignKey(string keyName) {
		waitingForKey = true;
		StartCoroutine(DotAnimation(keyName));

		yield return WaitForKey();
		waitingForKey = false;

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

		StopCoroutine("DotAnimation");
		buttons.Find("Button-" + keyName.Capitalize()).Find("Key").GetComponent<Text>().text = buttonString;
		PlayerPrefs.SetString(keyName + "Key", buttonString);
		PlayerPrefs.Save();

		yield return null;
	}
}
