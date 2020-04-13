using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float health = 100;
	[SerializeField] float speed = 0.4f;
	public float damage = 5;
	void Start () {
		
	}

	void LateUpdate() {
		// 'Watch' the player, flip enemy. | Little movement
		if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= 4) {
			if (
				(player.transform.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x > 0) ||
				(player.transform.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x < 0)
			)
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

			transform.Translate(Vector2.up * speed * Time.deltaTime);
		}
	}

	public void Hurt(int damage) {
		health -= damage;
		gameObject.GetComponent<Animator>().Play("SlimeDarkDamage");
		Debug.Log("Current health " + health);
		if (health <= 0) this.gameObject.SetActive(false);
	}

}
