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
	public bool isInAnimation = false;  // Use like mutex lock/unlock other animations/actions
	public bool canMove = true;
	private bool isAttacking = false;
	
	[SerializeField] GameObject specialAttack;  // Prefab
	public bool attackPowerUP = false;
	public bool defensePowerUP = false;
	public bool doubleCoinsPowerUP = false;

	private bool canDoubleJump;
	Rigidbody2D currentRigidBody;

	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
		specialAttack = Instantiate(specialAttack, attackPoint.position, attackPoint.rotation);  //, transform);
		specialAttack.SetActive(false);
	}

	void Update() {
		bool canShoot = false;
		if (attackPowerUP) {
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - attackPoint.position;
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			if (
				(direction == Direction.right && rotZ >= -15 && rotZ <= 115) ||
				(direction == Direction.left && ( (rotZ >= 80 && rotZ <= 180) || rotZ <= -150))
			) {
				attackPoint.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
				canShoot = true;
			} // else Debug.Log(rotZ);
			
		}

		if (Input.GetButtonDown("Fire1") && !isAttacking && canMove && isGrounded) {
			isAttacking = true;
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_DoublePunch");
			if (attackPowerUP && canShoot)	
				specialAttack.transform.GetChild(0).GetComponent<SpecialAttack>().Spawn(attackPoint.position, attackPoint.rotation);
			StartCoroutine("AttackHandler");
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackrangeX, attackrangeY, 1));
	}

	void FixedUpdate(){
		
		if ((Input.GetKey ("d") || Input.GetKey ("right")) && canMove && !isInAnimation) {
			if(direction != Direction.right) {
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				direction = Direction.right;
			}
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (moveSpeed, currentRigidBody.velocity.y);
		} else if ((Input.GetKey ("a") || Input.GetKey ("left")) && canMove && !isInAnimation) {
			if(direction != Direction.left) {
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				direction = Direction.left;
			}
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (-moveSpeed, currentRigidBody.velocity.y);
		}else {
			if (!isAttacking && canMove && !isInAnimation) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Idle");
		}

		if (isGrounded == true) canDoubleJump = true;

		if((Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up")) && canMove && !isInAnimation){
			if (isGrounded) currentRigidBody.velocity = new Vector2 (currentRigidBody.velocity.x, jumpHeight);
			else { 
				if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up")){
					if (canDoubleJump == true) {
						canDoubleJump = false;
						SoundManager.instance.Play("PlayerJump");
						// this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
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

	private void Hurt(float damage, Transform enemyTransform) {
		// Push away the player (Left,Right check)
		Vector2 relativePoint = transform.InverseTransformPoint(enemyTransform.position);
		float pushAway = Random.Range(moveSpeed * 2, moveSpeed * 3);
		if (relativePoint.x < 0.0) { // Object is to the left
			if (direction != Direction.right) pushAway *= -1;
		} else if (relativePoint.x > 0.0) {  // Object is to the right
			if (direction == Direction.right) pushAway *= -1;
		}

		currentRigidBody.velocity = new Vector2(pushAway, currentRigidBody.velocity.y);

		StartCoroutine("HurtHandler");  // Call coorutine for 'sleep'
		health -= defensePowerUP ? (int)(damage/2) : damage;  // Reduce the healt
		PlayerStats.Health = health;
		healthText.text = "Health: " + (health <= 0 ? "0" : health.ToString());
	}

	IEnumerator HurtHandler() {
		isInAnimation = true;
		// Play the hurt anymation
		gameObject.GetComponent<Animator>().Play("Owlet_Monster_Hurt");
		SoundManager.instance.Play("PlayerHurt");
		yield return new WaitForSeconds(0.3f);
		if (health <= 0) {
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_Death");
			yield return new WaitForSeconds(0.5f);
			this.gameObject.SetActive(false);
			SceneController.instance.LoadScene("GameOver");
		}
		isInAnimation = false;
	}

	IEnumerator AttackHandler() {
		Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackrangeX, attackrangeY), 0, enemies);
		for (int i = 0; i < enemiesToDamage.Length; i++) enemiesToDamage[i].gameObject.GetComponent<Enemy>().Hurt(15);
		yield return new WaitForSeconds(1.0f);
		isAttacking = false;
	}

	IEnumerator PowerUPHandler() {
		isInAnimation = true;
		gameObject.GetComponent<Animator>().Play("Owlet_Monster_JumpPowerUP");
		SoundManager.instance.Play("PlayerPowerUP");
		yield return new WaitForSeconds(1.0f);
		isInAnimation = false;
	}

	public void PowerUP(PowerUP.PowerUPType type) {
		if (type == global::PowerUP.PowerUPType.defense) defensePowerUP = true;
		else if (type == global::PowerUP.PowerUPType.attack) attackPowerUP = true;
		else if (type == global::PowerUP.PowerUPType.coins) doubleCoinsPowerUP = true;

		StartCoroutine("PowerUPHandler");
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			if (!isAttacking) Hurt(collision.collider.GetComponent<Enemy>().damage, collision.collider.transform);
		}
	}
}
