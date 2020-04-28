using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {

	public static GameInputManager instance;

	public KeyCode jump { get; set; }
	public KeyCode left { get; set; }
	public KeyCode right { get; set; }

	void Awake() {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		// From unity page, set 'default' values
		// Fetch name (string) from the PlayerPrefs (set these Playerprefs in another script). If no string exists, the default is "No Name"
		// m_PlayerName = PlayerPrefs.GetString("Name", "No Name");

		jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "W"));
		left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
	}



}
