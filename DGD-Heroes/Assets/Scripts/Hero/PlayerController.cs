using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float jumpHeight = 6f;

	// This is also can be replaced by PlayStats, like PowerUPs
	// Keep only for Serialize reason.
	[SerializeField] float health = 100;

	[SerializeField] Transform attackPoint;
	[SerializeField] LayerMask enemies;
	[SerializeField] float attackrangeX;
	[SerializeField] float attackrangeY;
	[SerializeField] float damage = 50;

	public enum Direction {left, right}
	public Direction direction = Direction.right;	// Just for readable code

	public bool isGrounded = false;
	public bool isInAnimation = false;  			// Use like mutex lock/unlock other animations/actions
	public bool canMove = true;
	private bool isAttacking = false;

	[SerializeField] GameObject specialAttack;      // Prefab
	[SerializeField] LayerMask groundLayer;

	// Move GetKey - GetKeyDown from FixedUpdate() to Update()
	// https://www.google.com/search?q=unity+getkeydown+fixedupdate
	private bool leftKeyPress = false;
	private bool rightKeyPress = false;
	private bool canDoubleJump = false;
	
	Rigidbody2D currentRigidBody;

	void Start () {
		SoundManager.instance.Play("StartLevel");

		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
		specialAttack = Instantiate(specialAttack, attackPoint.position, attackPoint.rotation);  //, transform);
		specialAttack.SetActive(false);

		// Recovery data from PlayerStat
		if (PlayerStats.Health != 0) health = PlayerStats.Health;
	}

	/*
	 * If the user have PowerUp attack goin to calculate the direction of SpecialAttack base on mouse position.
	 * First of all calculate the delta by mainCamera and mousePosition
	 * Check if position of the mouse is 'front' of the player and the rotation is not pointing the groud.
	 * Use RayCast with groundLayer and check if the Fireball can collide with the Ground. (It's like collider control in SpecialAttack but limited to the ground).
	 * If all it's ok set canShoot to true.
	 * rotZ - 90 because we have the special attack pivot rotate (for follow up Translate)
	 * If we can't use the special attack just do a 'normal' punch :)
	 * Obviously the player !currently attacking, can attack on air, and should be in move codition.
	 */
	void Update() {
		bool canShoot = false;
		if (PlayerStats.AttackPowerUP) {
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - attackPoint.position;
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			if(
				( direction == Direction.right && ((rotZ >= 0 && rotZ <= 92) || (rotZ <= 0 && rotZ >= -108)) ) ||
				( direction == Direction.left && ((rotZ >= 92 && rotZ <= 180) || (rotZ >= -180 && rotZ <= -90)) )
			) {
				attackPoint.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
				// Check if the fireball can collide with the Ground
				RaycastHit2D groundHitted = Physics2D.Raycast(attackPoint.position, attackPoint.up, 0.7f, groundLayer);
				if(groundHitted.collider == null) canShoot = true;
			}
		}

		if (Input.GetButtonDown("Fire1") && !isAttacking && canMove && isGrounded) {
			isAttacking = true;
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_DoublePunch");
			if (PlayerStats.AttackPowerUP && canShoot)
				specialAttack.transform.GetChild(0).GetComponent<SpecialAttack>().Spawn(attackPoint.position, attackPoint.rotation);
			StartCoroutine("AttackHandler");
		}

		if (Input.GetKey(GameInputManager.instance.right)) {
			leftKeyPress = false;
			rightKeyPress = true;
		}
		else if (Input.GetKey(GameInputManager.instance.left)){
			leftKeyPress = true;
			rightKeyPress = false;
		}

		if (isGrounded) canDoubleJump = true;

		if (Input.GetKey(GameInputManager.instance.jump) && canMove && !isInAnimation) {
			if (isGrounded) currentRigidBody.velocity = new Vector2(currentRigidBody.velocity.x, jumpHeight);
			else {
				if (Input.GetKeyDown(GameInputManager.instance.jump)) {
					if (canDoubleJump) {
						canDoubleJump = false;
						SoundManager.instance.Play("PlayerJump");
						currentRigidBody.velocity = new Vector2(currentRigidBody.velocity.x, jumpHeight);
					}
				}
			}
		}

	}

	// Just for see the range of 'normal' punch attack.
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackrangeX, attackrangeY, 1));
	}

	/*
	 * https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
	 * Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
     *
     * MonoBehaviour.FixedUpdate has the frequency of the physics system; it is called every fixed frame-rate frame.
     * Compute Physics system calculations after FixedUpdate. 0.02 seconds (50 calls per second) is the default time between calls.
	 *
	 * Use FixedUpdate for manage the movement of the player: left, right, jump
	 *
	 * For left and right check if getKey (from GameInputManager instance) is True, the player can move and is not in animation state.
	 * Then "Flip" the player if direction Enum not matching
	 * Play the run animation
	 * Change velocity for move the player
	 *
	 * For jump and double jump the logic is pretty the same of left, right movement. Just use a flag variable for double jump
	 * Edit: 18/05. Jump and double jump was moved onUpdate()
	 *
	 */
	void FixedUpdate(){

		if (rightKeyPress && canMove && !isInAnimation) {
			rightKeyPress = false;
			if (direction != Direction.right) {
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				direction = Direction.right;
			}
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (moveSpeed, currentRigidBody.velocity.y);
		} else if (leftKeyPress && canMove && !isInAnimation) {
			leftKeyPress = false;
			if (direction != Direction.left) {
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * - 1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				direction = Direction.left;
			}
			if (!isAttacking) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Run");
			currentRigidBody.velocity = new Vector2 (-moveSpeed, currentRigidBody.velocity.y);
		}else {
			if (!isAttacking && canMove && !isInAnimation) gameObject.GetComponent<Animator> ().Play ("Owlet_Monster_Idle");
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
		Vector2 relativePoint = transform.InverseTransformPoint(enemyTransform.position);	// Push away the player (Left,Right check)
		float pushAway = Random.Range(moveSpeed * 2, moveSpeed * 3);
		if (relativePoint.x < 0.0) { 														// Object is to the left
			if (direction != Direction.right) pushAway *= -1;
		} else if (relativePoint.x > 0.0) {  												// Object is to the right
			if (direction == Direction.right) pushAway *= -1;
		}

		currentRigidBody.velocity = new Vector2(pushAway, currentRigidBody.velocity.y);

		StartCoroutine("HurtHandler");
		health -= PlayerStats.DefensePowerUp ? (int)(damage/2) : damage;  	// Reduce the healt
		PlayerStats.Health = health;
		GameSingletonUI.instance.healthText.text = "Health: " + (health <= 0 ? "0" : health.ToString().PadLeft(3, '0'));
		if (health <= 15) GameSingletonUI.instance.healthText.color = new Color32(153, 17, 17, 255);
	}

	/*
	 * The following Handler coorutine with WaitForSeconds are use especially for animation.
	 * Create a state machine with flag variabile isInAnimation
	 * Thats because if we don't use the isInAnimation the Play of (ex.) Owlet_Monster_Hurt can be replace from Run of Idle animation.
	*/

	IEnumerator HurtHandler() {
		isInAnimation = true;
		gameObject.GetComponent<Animator>().Play("Owlet_Monster_Hurt");
		SoundManager.instance.Play("PlayerHurt");
		yield return new WaitForSeconds(0.3f);
		if (health <= 0) {
			SoundManager.instance.Play("Lose");
			gameObject.GetComponent<Animator>().Play("Owlet_Monster_Death");
			yield return new WaitForSeconds(0.5f);
			
			Destroy(gameObject);
			Destroy(TimerCountdown.instance.gameObject);
			Destroy(GameSingletonUI.instance.gameObject);

			SceneController.instance.LoadScene("GameOver");
		}
		isInAnimation = false;
	}

	IEnumerator AttackHandler() {
		Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackrangeX, attackrangeY), 0, enemies);
		for (int i = 0; i < enemiesToDamage.Length; i++) enemiesToDamage[i].gameObject.GetComponent<Enemy>().Hurt(damage);
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
		if (type == global::PowerUP.PowerUPType.defense) PlayerStats.DefensePowerUp = true;
		else if (type == global::PowerUP.PowerUPType.attack) PlayerStats.AttackPowerUP = true;
		else if (type == global::PowerUP.PowerUPType.coins) PlayerStats.DoubleCoinsPowerUP = true;

		GameSingletonUI.instance.PowerUPs.Find(type.ToString().Capitalize()).GetComponent<Image>().ChangeAlpha(1f);
		StartCoroutine("PowerUPHandler");
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			if (!isAttacking && !isInAnimation) Hurt(collision.collider.GetComponent<Enemy>().damage, collision.collider.transform);
		}
	}

	/*
	void OnCollisionStay2D(Collision2D collision) {
		if (collision.collider.tag == "Enemy") {
			if (!isAttacking) Hurt(collision.collider.GetComponent<Enemy>().damage, collision.collider.transform);
		}
	}
	*/
}
