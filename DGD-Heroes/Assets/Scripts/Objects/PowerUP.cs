using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUP : MonoBehaviour {

	public enum PowerUPType { attack, defense, coins }
	public PowerUPType type;

	[SerializeField] GameObject powerUPGameObject;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerController>().PowerUP(type);
			transform.parent.gameObject.SetActive(false);

			powerUPGameObject.transform.Find(type.ToString().Capitalize()).GetComponent<Image>().ChangeAlpha(1f);
		}
	}

}
