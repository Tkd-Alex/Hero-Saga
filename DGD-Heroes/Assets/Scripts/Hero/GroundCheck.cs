using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	GameObject Player;

	void Start () {
		Player = gameObject.transform.parent.gameObject;
	}

	/*
	 * Box collider base on bottom of the player.
	 * If the box collide with the Ground tag
	 * Play the 'Dust' ParticleSystem
	 * Set PlayerController flag to true.
	 */
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag == "Ground") {
			this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
			Player.GetComponent<PlayerController>().isGrounded = true;
			Player.GetComponent<PlayerController>().canMove = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.tag == "Ground") Player.GetComponent<PlayerController> ().isGrounded = false;
	}

}
