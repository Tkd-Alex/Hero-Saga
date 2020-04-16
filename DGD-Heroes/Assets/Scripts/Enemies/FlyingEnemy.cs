using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy {

	private bool isSleeping = true;

	void LateUpdate() {
		// 'Watch' the player, flip enemy. | Little movement
		if (
			(Math.Abs(player.transform.position.x - gameObject.transform.position.x) >= 0.05) && 
			Vector2.Distance(gameObject.transform.position, player.transform.position) <= playerDistance
		) {
			Move();
			WakeUpOrSleep();
		}
	}

	private void WakeUpOrSleep() {
		if (isSleeping) {
			gameObject.GetComponent<Animator>().Play("BatFlying");
			isSleeping = false;
		}
	}

}
