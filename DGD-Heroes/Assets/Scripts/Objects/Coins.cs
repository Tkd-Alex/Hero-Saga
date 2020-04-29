using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			CoinsManager.instance.IncreaseCoins();
			if(other.gameObject.GetComponent<PlayerController>().doubleCoinsPowerUP) CoinsManager.instance.IncreaseCoins();
			SoundManager.instance.Play("CoinsCollect");
			this.gameObject.SetActive(false);
		}
	}

	public void Spawn(Vector2 futurePosition, Quaternion rotation) {
		transform.Spawn(futurePosition, rotation);
	}

}
