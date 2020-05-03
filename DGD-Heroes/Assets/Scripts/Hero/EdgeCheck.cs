using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCheck : MonoBehaviour {
	/*
	 * EdgeCheck class was created for "fix" a bug.
	 * We have two little collider, left/right attached to the player.
	 * If we hit the ground with 'Edge' collider but we are currently flying and the player is not on animatio state.
	 * Send canMove flag to flag and unlock after 0.3f only if the player is not grounded.
	 * Why this? Where was the bug? Try to exaplain:
	 *
	 * 					__________
	 * 		          |
	 * 		         +|
	 * 		__________|
	 *
	 * Image that the plus (+) is our player and the pipe/dash is our tiles.
	 * If the player are currently notGrounded and the user are currently press the 'right' button.
	 * The player is locked on air and keep trying to walk
	 */

	GameObject Player;

	void Start() {
		Player = gameObject.transform.parent.gameObject;
	}
	void OnCollisionEnter2D(Collision2D collision) {
		// Debug.Log("Collision enter");
		GroundEdge(collision);
	}

	void OnCollisionStay2D(Collision2D collision) {
		// Debug.Log("Collision stay");
		GroundEdge(collision);
	}

	void GroundEdge(Collision2D collision) {
		if (collision.collider.tag == "Ground")
			if (
				!Player.GetComponent<PlayerController>().isGrounded &&
				!Player.GetComponent<PlayerController>().isInAnimation
			) {
				Player.GetComponent<PlayerController>().canMove = false;
				StartCoroutine("UnlockMove");
			}
	}

	IEnumerator UnlockMove() {
		yield return new WaitForSeconds(0.3f);  // Prevent control disable
		if(!Player.GetComponent<PlayerController>().isGrounded)
			Player.GetComponent<PlayerController>().canMove = true;
	}
}
