using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	GameObject Player;
	// Use this for initialization
	void Start () {
		Player = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag == "Ground") {
			Player.GetComponent<PlayerController> ().isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.tag == "Ground") {
			Player.GetComponent<PlayerController> ().isGrounded = false;
		}
	}

}
