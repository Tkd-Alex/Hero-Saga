using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			CoinsManager.instance.ChangeScore (1);
			Destroy (this.gameObject);
		}
	}
}
