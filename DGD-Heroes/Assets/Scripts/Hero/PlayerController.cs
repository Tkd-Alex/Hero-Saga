using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 6f;

	[SerializeField] float health = 100;

	[SerializeField] GameObject pouchCheck;
	
	public bool isAttacking = false;
	public bool isHurted = false;
	public bool isGrounded = false;
	
	private bool canDoubleJump;
	Rigidbody2D currentRigidBody;

	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
		pouchCheck.SetActive(false);
	}

	void Update() {
		if(Input.GetButtonDown("Fire1") && !isAttacking && !isHurted && isGrounded) {
			isAttacking = true;
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_DoublePunch");
			StartCoroutine("DoPunch");
		}
	}

	IEnumerator DoPunch() {
		pouchCheck.SetActive(true);
		yield return new WaitForSeconds(1.0f);
		isAttacking = false;
		pouchCheck.SetActive(false);
	}

	IEnumerator DelayHurt() {
		yield return new WaitForSeconds(0.3f);
		isHurted = false;
	}

	void FixedUpdate(){
		
		if ((Input.GetKey ("d") || Input.GetKey ("right")) && !isHurted) {
			// gameObject.GetComponent<SpriteRenderer> ().flipX = false;
			if(gameObject.transform.localScale.x < 0) gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.y);
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (moveSpeed, currentRigidBody.velocity.y);
		} else if ((Input.GetKey ("a") || Input.GetKey ("left")) && !isHurted) {
			// gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			if (gameObject.transform.localScale.x > 0)  gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.y);
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (-moveSpeed, currentRigidBody.velocity.y);
		} else {
			if (!isAttacking && !isHurted) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Idle");
		}

		if (isGrounded == true) canDoubleJump = true;

		if((Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")) && !isHurted){
			if (isGrounded) currentRigidBody.velocity = new Vector2 (currentRigidBody.velocity.x, jumpHeight);
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

		// Prevent out of screen movements ...
		Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
 
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
			transform.position.y, 
			transform.position.z
		);	

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			if (!isAttacking && !isHurted) {
				isHurted = true;  // My 'current-state' flag
				// Push away the player (Left,Right check)
				if (gameObject.transform.localScale.x > 0) currentRigidBody.velocity = new Vector2(-moveSpeed * 2, currentRigidBody.velocity.y);
				else currentRigidBody.velocity = new Vector2(moveSpeed * 2, currentRigidBody.velocity.y);
				// Play the hurt anymation
				gameObject.GetComponent<Animator>().Play("Owlet_Monster_Hurt");
				StartCoroutine("DelayHurt");  // Call coorutine for 'sleep'
				health -= collision.collider.GetComponent<Enemy>().damage;  // Reduce the healt
				Debug.Log("Current health " + health);
				if(health <= 0) gameObject.GetComponent<Animator>().Play("Owlet_Monster_Death");
			}
		}
	}
}
