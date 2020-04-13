using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsManager : MonoBehaviour {

	public static CameraBoundsManager instance;

	[SerializeField] float lefTimit;
	[SerializeField] float rightLimit;
	[SerializeField] float bottomLimit;
	[SerializeField] float topLimit;

	// Use this for initialization
	void Start() {
		if (instance == null)
			instance = this;
	}

	public float getLefTimit() { return lefTimit; }
	public float getRightLimit() { return rightLimit; }
	public float getBottomLimit() { return bottomLimit; }
	public float getTopLimit() { return topLimit; }

}
