using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {

	public static CoinsManager instance;
	[SerializeField] Text coinsCollectText;
	int coinsCollect;

	void Awake () {
		if (instance == null)
			instance = this;
	}

	public void ChangeCoins (int value) {
		coinsCollect += value;
		coinsCollectText.text = "Coins: " + coinsCollect.ToString ();
	}

	public void IncreaseCoins() {
		PlayerStats.IncCoins();
		coinsCollect += 1;
		coinsCollectText.text = "Coins: " + coinsCollect.ToString();
	}
}
