using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	GameObject Player;
	// Use this for initialization
	void Start () {
		Player = gameObject.transform.parent.gameObject;
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag == "Ground") {
			this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
			Player.GetComponent<PlayerController> ().isGrounded = true;
			Player.GetComponent<PlayerController>().canMove = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.tag == "Ground") Player.GetComponent<PlayerController> ().isGrounded = false;
	}

}
