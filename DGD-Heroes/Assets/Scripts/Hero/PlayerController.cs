using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 6f;

	[SerializeField] float health = 100;
	[SerializeField] Text healthText;

	[SerializeField] Transform attackPoint;
	[SerializeField] LayerMask enemies;
	[SerializeField] float attackrangeX;
	[SerializeField] float attackrangeY;

	private enum Direction {left, right}
	private Direction direction = Direction.right;

	public bool isGrounded = false;
	private bool isAttacking = false;
	private bool canMove = true;  // Use like mutex lock/unlock other animations/actions

	[SerializeField] GameObject specialAttackPrefab;  // Prefab
	public bool attackPowerUP = false;
	public bool defensePowerUP = false;

	private bool canDoubleJump;
	Rigidbody2D currentRigidBody;

	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
		specialAttackPrefab = Instantiate(specialAttackPrefab, attackPoint.position, attackPoint.rotation, transform);
		specialAttackPrefab.SetActive(false);
	}

	void Update() {
		if(Input.GetButtonDown("Fire1") && !isAttacking && canMove && isGrounded) {
			isAttacking = true;
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_DoublePunch");
			if(attackPowerUP) {
				Vector3 spawnPositionAttack = attackPoint.position;
				spawnPositionAttack.x = gameObject.transform.localScale.x < 0 ? spawnPositionAttack.x - 0.8f : spawnPositionAttack.x + 0.8f;  // offset for graphic fireball
				specialAttackPrefab.transform.SetPositionAndRotation(spawnPositionAttack, attackPoint.rotation);
				specialAttackPrefab.SetActive(true);
			}
			StartCoroutine("AttackHandler");
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackrangeX, attackrangeY, 1));
	}

	void FixedUpdate(){
		
		if ((Input.GetKey ("d") || Input.GetKey ("right")) && canMove) {
			// gameObject.GetComponent<SpriteRenderer> ().flipX = false;

			if(direction != Direction.right) {
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				direction = Direction.right;
			}
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (moveSpeed, currentRigidBody.velocity.y);
		} else if ((Input.GetKey ("a") || Input.GetKey ("left")) && canMove) {
			// gameObject.GetComponent<SpriteRenderer> ().flipX = true;
			if(direction != Direction.left) {
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);;
				direction = Direction.left;
			}
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (-moveSpeed, currentRigidBody.velocity.y);
		}else {
			if (!isAttacking && canMove) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Idle");
		}

		if (isGrounded == true) canDoubleJump = true;

		if((Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")) && canMove){
			if (isGrounded) currentRigidBody.velocity = new Vector2 (currentRigidBody.velocity.x, jumpHeight);
			else { 
				if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up")){
					if (canDoubleJump == true) {
						canDoubleJump = false;
						SoundManager.instance.Play("PlayerJump");
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

	void Hurt(float damage) {
		// Push away the player (Left,Right check)
		if (gameObject.transform.localScale.x > 0) currentRigidBody.velocity = new Vector2(-moveSpeed * 2, currentRigidBody.velocity.y);
		else currentRigidBody.velocity = new Vector2(moveSpeed * 2, currentRigidBody.velocity.y);
		StartCoroutine("HurtHandler");  // Call coorutine for 'sleep'
		health -= defensePowerUP ? (int)(damage/2) : damage;  // Reduce the healt
		Debug.Log("Current health " + health);
		healthText.text = "Health: " + (health <= 0 ? "0" : health.ToString());
	}


	IEnumerator HurtHandler() {
		canMove = false;
		// Play the hurt anymation
		gameObject.GetComponent<Animator>().Play("Owlet_Monster_Hurt");
		SoundManager.instance.Play("PlayerHurt");
		yield return new WaitForSeconds(0.3f);
		if (health <= 0) {
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_Death");
			yield return new WaitForSeconds(0.5f);
			this.gameObject.SetActive(false);
		}
		canMove = true;
	}

	IEnumerator AttackHandler() {
		Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackrangeX, attackrangeY), 0, enemies);
		for (int i = 0; i < enemiesToDamage.Length; i++) enemiesToDamage[i].gameObject.GetComponent<Enemy>().Hurt(15);
		yield return new WaitForSeconds(1.0f);
		isAttacking = false;
	}

	IEnumerator PowerUPHandler() {
		canMove = false;
		gameObject.GetComponent<Animator>().Play("Owlet_Monster_JumpPowerUP");
		SoundManager.instance.Play("PlayerPowerUP");
		yield return new WaitForSeconds(1.0f);
		canMove = true;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			if (!isAttacking) Hurt(collision.collider.GetComponent<Enemy>().damage);
		}
	}

	public void PowerUP(string powerup) {
		if (powerup == "defense") defensePowerUP = true;
		else if (powerup == "attack") attackPowerUP = true;

		StartCoroutine("PowerUPHandler");
	}
}
