﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			CoinsManager.instance.IncreaseCoins();
			this.gameObject.SetActive(false);
		}
	}

	void OnBecameInvisibile() {
		this.gameObject.SetActive(false);
	}

}