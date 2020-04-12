using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	/*
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Player")) {
			CoinsManager.instance.ChangeScore (1);
			// Destroy (this.gameObject);
			this.gameObject.SetActive(false);
		}
	}
	*/

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			CoinsManager.instance.ChangeScore(1);
			this.gameObject.SetActive(false);
		}
	}

}
