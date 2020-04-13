using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float health = 100;
	public float damage = 1;
	void Start () {
		
	}

	void LateUpdate() {
		// 'Watch' the player, flip enemy.
		if( 
			(player.transform.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x > 0) || 
			(player.transform.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x < 0) 
		)
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}

	public void Hurt(int damage) {
		health -= damage;
		gameObject.GetComponent<Animator>().Play("SlimeDarkDamage");
		Debug.Log("Current health " + health);
		if (health <= 0) this.gameObject.SetActive(false);
	}

}
