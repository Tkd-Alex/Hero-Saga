using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSingletonUI : MonoBehaviour {
	[SerializeField] public Text healthText;
	[SerializeField] public Text coinsCollectText;
	[SerializeField] public Text textSecondsLeft;
	[SerializeField] public Transform PowerUPs;

	public static GameSingletonUI instance;
	void Awake() {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
}
