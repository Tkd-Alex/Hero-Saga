using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChangeLevel : MonoBehaviour {

	[SerializeField] bool gameOver = false;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			if (gameOver) {
				PlayerStats.IncreaseExtraPoint(250);
				SoundManager.instance.Play("Win");
				SceneController.instance.LoadScene("GameOver");
				// Another fps drop here . The following Destroy was moved on Start of GameOver
				// Destroy(TimerCountdown.instance.gameObject);
				// Destroy(GameSingletonUI.instance.gameObject);
			} else {
				PlayerStats.IncreaseExtraPoint(150);
				SceneController.instance.LoadScene("Level2");
			}
		}
	}
}
