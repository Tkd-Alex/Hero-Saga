using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float speed = 0.6f;

	[SerializeField] float maxHealth = 100f;
	private float health;

	[SerializeField] GameObject healthBar;
	[SerializeField] Transform healthBarPoint;

	public float damage = 5;
	Rigidbody2D currentRigidBody;

	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
		healthBar = Instantiate(healthBar, healthBarPoint.position, healthBarPoint.rotation, transform);
		health = maxHealth;
		resizeHealthBar();
	}

	void LateUpdate() {
		// 'Watch' the player, flip enemy. | Little movement
		if ((Math.Abs(player.transform.position.x - gameObject.transform.position.x) >= 0.05) &&  Vector2.Distance(gameObject.transform.position, player.transform.position) <= 4) {
			if (
				(player.transform.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x > 0) ||
				(player.transform.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x < 0)
			) gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			
			currentRigidBody.velocity = new Vector2(gameObject.transform.localScale.x < 0 ? speed : -speed, currentRigidBody.velocity.y);
		}
	}

	private void resizeHealthBar() {
		Vector3 localScale = healthBar.gameObject.transform.localScale;
		localScale.x = (float)((1.5 * health) / maxHealth);
		healthBar.gameObject.transform.localScale = localScale;
	}


	public void Hurt(int damage) {
		health -= damage;
		resizeHealthBar();
		SoundManager.instance.Play("HitEnemy");
		gameObject.GetComponent<Animator>().Play("Damage");
		if (health <= 0) this.gameObject.SetActive(false);
	}

}
