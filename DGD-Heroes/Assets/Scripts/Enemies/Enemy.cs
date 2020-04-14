using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float health = 100;
	[SerializeField] float speed = 0.6f;
	
	Rigidbody2D currentRigidBody;
	public float damage = 5;
	
	void Start () {
		currentRigidBody = gameObject.GetComponent<Rigidbody2D>();
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

	public void Hurt(int damage) {
		health -= damage;
		SoundManager.instance.Play("HitEnemy");
		gameObject.GetComponent<Animator>().Play("SlimeDarkDamage");
		Debug.Log("Current health " + health);
		if (health <= 0) this.gameObject.SetActive(false);
	}

}
