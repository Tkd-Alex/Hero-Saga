using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour {

	public enum PowerUPType { attack, defense }
	public PowerUPType type;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerController>().PowerUP(type);
			transform.parent.gameObject.SetActive(false);
		}
	}

}
