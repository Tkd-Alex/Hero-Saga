using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	[SerializeField] float speed = 10.0f;
	[SerializeField] float lifeTime = 1.0f;

	void Start() {
		Invoke("DestroyRock", lifeTime);
	}

	void Update () {
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0);
		if(hitInfo.collider != null) {
			if(hitInfo.collider.CompareTag("Enemy")) hitInfo.collider.gameObject.GetComponent<Enemy>().Hurt(25);
			DestroyRock();
		}else transform.Translate(Vector2.up * speed * Time.deltaTime);
	}

	void DestroyRock() {
		this.gameObject.SetActive(false);
	}
}
