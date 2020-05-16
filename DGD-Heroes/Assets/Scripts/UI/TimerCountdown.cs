using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerCountdown : MonoBehaviour {

	[SerializeField] int secondsLeft;
	bool mutex = false;

	public static TimerCountdown instance;
	void Awake () {
		UpdateUITextFromSeconds();

		// TimerCountdown converted to singleton because we need the same instance also in the Level2
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		if (mutex == false && secondsLeft > 0)
			StartCoroutine(TimerTake());
		if (secondsLeft <= 0) {
			SoundManager.instance.Play("Lose");

			StopAllCoroutines();  // Just for make sure that the cooruting is stopped (should be)
			
			Destroy(gameObject);
			Destroy(GameSingletonUI.instance.gameObject);
			
			SceneController.instance.LoadScene("GameOver");
		}
	}

	void UpdateUITextFromSeconds(){
		// Create a TimeSpan object from seconds float variable
		TimeSpan t = TimeSpan.FromSeconds( secondsLeft );
		// Update the UI text with M:S
		GameSingletonUI.instance.textSecondsLeft.text = "Left: " + t.Minutes.ToString ().PadLeft (2, '0') + ":" + t.Seconds.ToString ().PadLeft (2, '0');
		if (secondsLeft <= 15) {
			SoundManager.instance.Play("Alarm");
			GameSingletonUI.instance.textSecondsLeft.color = new Color32(153, 17, 17, 255);
		}
	}

	/*
	 * Wait for 1 seconds with coorutin
	 * After that decrease secondsLeft
	 * Call UpdateUITextFromSeconds
	 * Save the value in PlayerStats
	 * Set mutex = false for allow Start TimerTake again from update method
	 */
	IEnumerator TimerTake(){
		mutex = true;
		yield return new WaitForSeconds (1);
		secondsLeft -= 1;

		UpdateUITextFromSeconds();
		PlayerStats.Time = secondsLeft;

		mutex = false;
	}
}
