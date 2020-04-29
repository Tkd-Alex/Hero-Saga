using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsManager : MonoBehaviour {
	/*
	 * Create a singleton just for store value reachable 'everywhere'
	 * Used a singleton because i need to serialize the value.
	 */

	public static CameraBoundsManager instance;

	[SerializeField] float lefTimit;
	[SerializeField] float rightLimit;
	[SerializeField] float bottomLimit;
	[SerializeField] float topLimit;

	void Start() {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
	}

	public float getLefTimit() { return lefTimit; }
	public float getRightLimit() { return rightLimit; }
	public float getBottomLimit() { return bottomLimit; }
	public float getTopLimit() { return topLimit; }

}
