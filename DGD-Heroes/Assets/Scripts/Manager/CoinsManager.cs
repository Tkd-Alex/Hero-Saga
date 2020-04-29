using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {

	/*
	 * Sorry, the following singleton was replaced by PlayerStats static class.
	 * http://www.crazyforcode.com/cant-static-class-singleton/

	public static CoinsManager instance;
	int coinsCollect;

	void Awake () {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
	}

	public void ChangeCoins (int value) {
		coinsCollect += value;
		GameSingletonUI.instance.coinsCollectText.text = "Coins: " + coinsCollect.ToString ();
	}

	public void IncreaseCoins() {
		PlayerStats.IncCoins();
		coinsCollect += 1;
		GameSingletonUI.instance.coinsCollectText.text = "Coins: " + coinsCollect.ToString();
	}
	*/
}
