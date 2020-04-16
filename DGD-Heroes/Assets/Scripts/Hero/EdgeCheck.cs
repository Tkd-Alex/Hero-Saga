using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCheck : MonoBehaviour {
	GameObject Player;

	void Start() {
		Player = gameObject.transform.parent.gameObject;
	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Ground")
			if (
				!Player.GetComponent<PlayerController>().isGrounded &&
				!Player.GetComponent<PlayerController>().isInAnimation
			) {
				Player.GetComponent<PlayerController>().canMove = false;
				StartCoroutine("unlockMove");
			}
	}

	IEnumerator unlockMove() {
		yield return new WaitForSeconds(0.3f);  // Prevent control disable
		if(!Player.GetComponent<PlayerController>().isGrounded)
			Player.GetComponent<PlayerController>().canMove = true;
	}
}
