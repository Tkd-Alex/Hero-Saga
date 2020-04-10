using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {

	public static CoinsManager instance;
	[SerializeField] Text coinsCollectText;
	int coinsCollect;

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
	}
	
	// Update is called once per frame
	public void ChangeScore (int value) {
		coinsCollect += value;
		coinsCollectText.text = coinsCollect.ToString ();
	}
}
