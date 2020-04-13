﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerCountdown : MonoBehaviour {

	[SerializeField] Text textSecondsLeft;
	[SerializeField] int secondsLeft;
	bool takingAway = false;

	void Start () {
		TimeSpan t = TimeSpan.FromSeconds( secondsLeft );
		textSecondsLeft.text = "Left: " + t.Minutes.ToString ().PadLeft (2, '0') + ":" + t.Seconds.ToString ().PadLeft (2, '0');
	}
	
	void Update () {
		if (takingAway == false && secondsLeft > 0)
			StartCoroutine (TimerTake ());
	}

	IEnumerator TimerTake(){
		takingAway = true;
		yield return new WaitForSeconds (1);
		secondsLeft -= 1;

		TimeSpan t = TimeSpan.FromSeconds( secondsLeft );
		textSecondsLeft.text = "Left: " + t.Minutes.ToString ().PadLeft (2, '0') + ":" + t.Seconds.ToString ().PadLeft (2, '0');
		takingAway = false;
	}
}