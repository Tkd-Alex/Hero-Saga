using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

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

	protected void Move() {
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}

	private void ResizeHealthBar() {
		Vector3 localScale = healthBar.gameObject.transform.localScale;
		localScale.x = (float)((1.5 * health) / maxHealth);
		healthBar.gameObject.transform.localScale = localScale;
	}


	public void Hurt(int damage) {
		health -= damage;
		ResizeHealthBar();
		SoundManager.instance.Play("HitEnemy");
		gameObject.GetComponent<Animator>().Play("Damage");
		if (health <= 0) {
			this.gameObject.SetActive(false);
			PlayerStats.IncKills();
		}
	}
}
