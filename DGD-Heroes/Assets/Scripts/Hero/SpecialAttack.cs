using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

	[SerializeField] float speed = 10.0f;
	[SerializeField] float lifeTime = 1.0f;
	[SerializeField] GameObject explosion;

	void Start() {
		explosion = Instantiate(explosion);
	}

	void OnEnable() {
		Invoke("DestroyThisAttack", lifeTime);
	}

	void Update () {
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0);
		if(hitInfo.collider != null) {
			explosion.transform.Spawn(transform.position, transform.rotation);
			Invoke("DisableExplosion", 0.5f);
			DestroyThisAttack();
			if (hitInfo.collider.CompareTag("Enemy")) hitInfo.collider.gameObject.GetComponent<Enemy>().Hurt(25);
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
