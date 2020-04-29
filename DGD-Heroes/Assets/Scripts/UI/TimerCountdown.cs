﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerCountdown : MonoBehaviour {

	[SerializeField] Text textSecondsLeft;
	[SerializeField] int secondsLeft;
	bool mutex = false;

	void Start () {
		UpdateUITextFromSeconds();
	}

	void Update () {
		if (mutex == false && secondsLeft > 0)
			StartCoroutine(TimerTake());
		if (secondsLeft <= 0) {
			StopAllCoroutines();  // Just for make sure that the cooruting is stopped (should be)
			SceneController.instance.LoadScene("GameOver");
		}
	}

	void UpdateUITextFromSeconds(){
		// Create a TimeSpan object from seconds float variable
		TimeSpan t = TimeSpan.FromSeconds( secondsLeft );
		// Update the UI text with M:S
		textSecondsLeft.text = "Left: " + t.Minutes.ToString ().PadLeft (2, '0') + ":" + t.Seconds.ToString ().PadLeft (2, '0');
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

		UpdateUITextFromSeconds()
		PlayerStats.Time = secondsLeft;

		mutex = false;
	}
}
