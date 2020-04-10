using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 6f;

	public bool isGrounded = false;
	private bool canDoubleJump;
	Rigidbody2D currentRigidBody;

	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		
		if (Input.GetKey ("d") || Input.GetKey ("right")) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (moveSpeed, currentRigidBody.velocity.y);
		} else if (Input.GetKey ("a") || Input.GetKey ("left")) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (-moveSpeed, currentRigidBody.velocity.y);
		} else {
			gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Idle");
		}

		if (isGrounded == true) canDoubleJump = true;

		if(Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")){
			if (isGrounded == true) currentRigidBody.velocity = new Vector2 (currentRigidBody.velocity.x, jumpHeight);
			else { 
				if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up")){
					if (canDoubleJump == true) {
						canDoubleJump = false;
						currentRigidBody.velocity = new Vector2 (currentRigidBody.velocity.x, jumpHeight);
					}
				}
			}	
			// gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Jump");
		}

		Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
 
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
			transform.position.y, 
			transform.position.z
		);	

	}

}
