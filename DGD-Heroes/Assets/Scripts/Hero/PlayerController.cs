using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 6f;

	public bool isGrounded = false;
	Rigidbody2D currentRigidBody;

	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		if(Input.GetKey("d") || Input.GetKey("right")){
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (moveSpeed, currentRigidBody.velocity.y);
		} 
		else if(Input.GetKey("a") || Input.GetKey("left")){
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (-moveSpeed, currentRigidBody.velocity.y);
		}
		else if(isGrounded == false) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Idle");

		if((Input.GetKey("space") || Input.GetKey("w")) && isGrounded == true){
			currentRigidBody.velocity = new Vector2 (currentRigidBody.velocity.x, jumpHeight);
			gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Jump");
		}
	}

}
