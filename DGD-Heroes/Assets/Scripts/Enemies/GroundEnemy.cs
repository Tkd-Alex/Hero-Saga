using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy {

	void LateUpdate() {
		// 'Watch' the player, flip enemy. | Little movement
		if (
			(Math.Abs(player.transform.position.x - gameObject.transform.position.x) >= 0.05) && 
			Vector2.Distance(gameObject.transform.position, player.transform.position) <= playerDistance
		) {
			Flip();
			Move();
		}
	}


	private void Flip() {
		if (
			(player.transform.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x > 0) ||
			(player.transform.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x < 0)
		) gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}

}
