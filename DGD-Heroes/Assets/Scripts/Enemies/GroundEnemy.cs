using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy {

	void LateUpdate() {
		if(transform.IsNearOther2D(player.transform, playerDistance) == true){
			Flip();
			Move();
		}
	}

	private void Flip() {
		transform.Flip2D(player.transform);
	}

}
