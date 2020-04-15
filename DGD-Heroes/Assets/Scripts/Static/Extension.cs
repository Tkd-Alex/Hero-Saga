using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension {

	public static void Spawn(this Transform trans, Vector2 futurePosition, Quaternion rotation) {
		trans.SetPositionAndRotation(futurePosition, rotation);
		trans.gameObject.SetActive(true);
	}
}
