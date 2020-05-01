using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonHandler : MonoBehaviour {

	public void Menu() {
		SceneController.instance.LoadScene("Menu");
	}

	public void Play() {
		PlayerStats.ResetAll();
		SceneController.instance.LoadScene("Level1");
	}

	public void Tutorial(string name) {
		SceneController.instance.LoadScene("Tutorial-" + name.Capitalize());
	}

	public void Credits() {
		SceneController.instance.LoadScene("Credits");
	}

	public void Scoreboard() {
		SceneController.instance.LoadScene("Scoreboard");
	}

	public void ChangeKeys() {
		SceneController.instance.LoadScene("ChangeKeys");
	}
}
