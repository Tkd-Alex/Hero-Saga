using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			// Singleton instance call was replaced by PlayerStats static class.
			// CoinsManager.instance.IncreaseCoins();
			// if(other.gameObject.GetComponent<PlayerController>().doubleCoinsPowerUP) CoinsManager.instance.IncreaseCoins();
			PlayerStats.IncreaseCoins();
			GameSingletonUI.instance.coinsCollectText.text = "Coins: " + PlayerStats.Coins.ToString().PadLeft(3, '0');
			SoundManager.instance.Play("CoinsCollect");
			this.gameObject.SetActive(false);
		}
	}

	public void Spawn(Vector2 futurePosition, Quaternion rotation) {
		transform.Spawn(futurePosition, rotation);
	}

}
