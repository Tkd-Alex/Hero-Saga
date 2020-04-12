using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawnManager : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] private GameObject coin;

	private GameObject[] coinsList;
	[SerializeField] private int maxCoinsOnScreen;

	int lastSpawnIndex = 0;

	// Use this for initialization
	void Start () {
		coinsList = new GameObject[maxCoinsOnScreen];
		StartCoroutine("SpawnCoins");
		StartCoroutine("DeactivateDistanceCoins");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnCoins() {
		while (true) {
			Vector2 futurePosition;
			Collider2D[] collider2Ds;
			bool nearAnotherCoin = false;
			bool noSpawn = false;

			yield return new WaitForSeconds(Random.Range(3, 5));

			int loops = 0;
			while(true){
				futurePosition.x = Random.value >= 0.5 ? player.transform.position.x + Random.Range(0.3f, 3.0f) : player.transform.position.x - Random.Range(0.3f, 0.5f);
				futurePosition.y = player.transform.position.y + Random.Range(0.5f, 2.8f);
				collider2Ds = Physics2D.OverlapCircleAll(futurePosition, 0.2f);
				for (int i = 0; i < collider2Ds.Length; i++) Debug.Log(collider2Ds[i].name);
				// Debug.Log("Spawn attempt at x: " + futurePosition.x + " y: " + futurePosition.x);
				if (collider2Ds.Length == 0){
					for (int i = 0; i < maxCoinsOnScreen; i++){
						if(coinsList[i] != null) {
							float distance = Vector2.Distance(coinsList[i].transform.position, player.transform.position);
							// Debug.Log("Loop: " + loops + " | Distance con i= " + i + " == " + distance);
							if (distance < 0.5) {
								nearAnotherCoin = true;
								break;
							}
						}
					}
					if(!nearAnotherCoin) break;
				}
				if (loops >= 10 && collider2Ds.Length == 0) break;
				else if(loops >= 25) {
					noSpawn = true;
					break;
				}
				else loops += 1;
			}
			if (!noSpawn) {
				if (lastSpawnIndex >= maxCoinsOnScreen) lastSpawnIndex = 0;
				if (coinsList[lastSpawnIndex] != null && coinsList[lastSpawnIndex].activeInHierarchy == false) {
					// Debug.Log("Position and setActive at: " + lastSpawnIndex);
					coinsList[lastSpawnIndex].transform.SetPositionAndRotation(futurePosition, Quaternion.identity);
					coinsList[lastSpawnIndex].SetActive(true);
				} else if (coinsList[lastSpawnIndex] == null) {
					// Debug.Log("Instanziate new element " + lastSpawnIndex);
					coinsList[lastSpawnIndex] = GameObject.Instantiate(coin, futurePosition, Quaternion.identity);
					// coinsList[lastSpawnIndex].SetActive(true);
				}
				lastSpawnIndex += 1;
			}
		}
	}

	IEnumerator DeactivateDistanceCoins()
	{
		while (true)
		{
			yield return new WaitForSeconds(5);

			for (int i = 0; i < maxCoinsOnScreen; i++)
				if(coinsList[i] != null && Vector2.Distance(coinsList[i].transform.position, player.transform.position) >= 4)
					coinsList[i].SetActive(false);
		}
	}
}
