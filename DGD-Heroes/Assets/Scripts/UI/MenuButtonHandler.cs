using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonHandler : MonoBehaviour {

	public void Menu() {
		SceneController.instance.LoadScene("Menu");
	}

	public void Play() {
		SceneController.instance.LoadScene("Level1");
	}

	public void Tutorial() {
		SceneController.instance.LoadScene("Tutorial");
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
