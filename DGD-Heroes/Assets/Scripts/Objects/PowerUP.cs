using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour {

	[SerializeField] string powerupName;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerController>().PowerUP(powerupName);
			transform.parent.gameObject.SetActive(false);
		}
	}

	void OnBecameInvisibile() {
		this.gameObject.SetActive(false);
	}

	void OnBecameVisibile() {
		this.gameObject.SetActive(true);
	}

}
