﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

	[SerializeField] float speed = 10.0f;
	[SerializeField] float lifeTime = 1.0f;
	[SerializeField] float damage = 25;
	[SerializeField] GameObject explosion;

	// Instantiate explosion object from prefab (Particles)
	void Start() {
		explosion = Instantiate(explosion);
	}

	void OnEnable() {
		Invoke("DestroyThisAttack", lifeTime);
	}

	/*
	 * Use RaycastHit2D for search any collision.
	 * If we have a collission spawn the particles effect
	 * Disable the particles after 0.5f seconds
	 * "Destroy" (setFalse) the current component
	 * If the collision is with Enemy tug, Hurt the enemeny with damage value.
	 * On collission mission move up the attack (rebember that the pivot on trasform is rotated)
	 */
	void Update() {
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0);
		// if(hitInfo.collider != null) {  // In this way if collide with PowerUP or coins will be destroyed
		if (hitInfo.collider != null && (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("Ground"))) { 
			explosion.transform.Spawn(transform.position, transform.rotation);
			Invoke("DisableExplosion", 0.5f);
			DestroyThisAttack();
			if (hitInfo.collider.CompareTag("Enemy")) hitInfo.collider.gameObject.GetComponent<Enemy>().Hurt(damage);
		} else transform.parent.Translate(Vector2.up * speed * Time.deltaTime);
	}

	void DisableExplosion() {
		explosion.SetActive(false);
	}

	void DestroyThisAttack() {
		transform.parent.gameObject.SetActive(false);
	}

	public void Spawn(Vector2 futurePosition, Quaternion rotation) {
		transform.parent.Spawn(futurePosition, rotation);
	}

}
