using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChangeLevel : MonoBehaviour {

	[SerializeField] bool gameOver = false;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			if (gameOver) {
				SceneController.instance.LoadScene("GameOver");
				Destroy(TimerCountdown.instance.gameObject);
				Destroy(GameSingletonUI.instance.gameObject);
			} else SceneController.instance.LoadScene("Level2");
		}
	}
}
