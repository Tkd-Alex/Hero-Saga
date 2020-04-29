using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy {

	private bool isSleeping = true;

	void LateUpdate() {
		if (transform.IsNearOther2D(player.transform, playerDistance) == true) {
			Move();
			WakeUpOrSleep();
		}
	}

	/*
	 * Flying enemy have also sleep animation ...
	 * Default state to default
	 * Wake up if the player is near by
	 */
	private void WakeUpOrSleep() {
		if (isSleeping) {
			gameObject.GetComponent<Animator>().Play("BatFlying");
			isSleeping = false;
		}
	}

}
