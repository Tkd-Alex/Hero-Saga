using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChangeLevel : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			SceneController.instance.LoadScene("Level2");
		}
	}
}
