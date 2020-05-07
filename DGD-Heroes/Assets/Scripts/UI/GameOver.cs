using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	[SerializeField] Text finalScoreText;
	[SerializeField] GameObject inputField;

	void Start () {
		finalScoreText.text = PlayerStats.CalculatePoints().ToString();

		// Debug.Log("Kills: " + PlayerStats.Kills.ToString());
		// Debug.Log("Coins: " + PlayerStats.Coins.ToString());
		// Debug.Log("Time: " + PlayerStats.Time.ToString());
		// Debug.Log("Health: " + PlayerStats.Health.ToString());
	}

	/*
	 * Easy, on SaveScore method get the text from UI
	 * If the string is empty return Unity modal error.
	 * Else, create a entry (with static class / save in PlayerPrefs) and return to Menu.
	 */
	public void SaveScore() {
		string name = inputField.GetComponent<Text>().text;
		if (!string.IsNullOrEmpty(name)) {
			ScoreManager.CreateEntry(name, PlayerStats.CalculatePoints());
			// BackToMenu();
			SceneController.instance.LoadScene("Scoreboard");
		} // else UnityEditor.EditorUtility.DisplayDialog("Error", "Please insert a name. If you don't want to save the score press the menu button.", "Ok");
	}

	public void BackToMenu() {
		SceneController.instance.LoadScene("Menu");
	}

}
