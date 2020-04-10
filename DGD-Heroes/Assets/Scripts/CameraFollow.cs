using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] float timeOffset;
	[SerializeField] Vector2 posOffset;

	[SerializeField] float lefTimit;
	[SerializeField] float rightLimit;
	[SerializeField] float bottomLimit;
	[SerializeField] float topLimit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 startPos = transform.position;  // Camera
		Vector3 endPos = player.transform.position; // Player 
		endPos.x += posOffset.x;
		endPos.y += posOffset.y;
		endPos.z = -10;

		transform.position = Vector3.Lerp (startPos, endPos, timeOffset * Time.deltaTime);
	
		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, lefTimit, rightLimit),
			Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
			transform.position.z
		);
	}
}
