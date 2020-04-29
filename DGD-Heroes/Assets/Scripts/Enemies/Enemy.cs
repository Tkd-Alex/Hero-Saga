using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	/*
	 * Each Enemy have:
	 * Player GameObject used for 'follow' the player base on position
	 * playerDistance float value used for enable the movement after distance reach
	 * speed float * Time.deltaTime used for the movement
	 * Multiple healt variable/components
	 * If the enemy hit the player, remove "damage" value from health player
	 */

	[SerializeField] protected GameObject player;
	[SerializeField] protected float playerDistance = 4f;
	[SerializeField] protected float speed = 0.6f;

	[SerializeField] float maxHealth = 100f;
	private float health;

	[SerializeField] GameObject healthBar;
	[SerializeField] Transform healthBarPoint;
	public float damage = 5;

	void Start () {
		healthBar = Instantiate(healthBar, healthBarPoint.position, healthBarPoint.rotation, transform);
		health = maxHealth;
		ResizeHealthBar();
	}

	// "Follow" the player, change position based on player position.
	protected void Move() {
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}

	private void ResizeHealthBar() {
		Vector3 localScale = healthBar.gameObject.transform.localScale;
		localScale.x = (float)((1.5 * health) / maxHealth);
		healthBar.gameObject.transform.localScale = localScale;
	}

	public void Hurt(float damage) {
		// The 'pushAway' code was copied from PlayerController
		Vector2 relativePoint = transform.InverseTransformPoint(player.transform.position);
		float pushAway = 2.5f;
		if (relativePoint.x < 0.0) {
			if (player.GetComponent<PlayerController>().direction != PlayerController.Direction.right) pushAway *= -1;
		} else if (relativePoint.x > 0.0) {
			if (player.GetComponent<PlayerController>().direction == PlayerController.Direction.right) pushAway *= -1;
		}
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(pushAway, gameObject.GetComponent<Rigidbody2D>().velocity.y);

		health -= damage;
		ResizeHealthBar();
		SoundManager.instance.Play("HitEnemy");
		gameObject.GetComponent<Animator>().Play("Damage");
		if (health <= 0) {
			this.gameObject.SetActive(false);
			PlayerStats.IncreaseKills();
		}
	}
}
