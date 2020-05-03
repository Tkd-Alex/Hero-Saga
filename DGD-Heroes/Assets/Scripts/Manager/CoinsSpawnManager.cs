using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawnManager : MonoBehaviour {

	/*
	 * Small summary of CoinsSpawnManager class:
	 * On start method Instantiate an array of GameObject with Coin prefab. (active: false)
	 * Start coorutine for Spawn and Deactive coins.
	 * IEnumerator SpawnCoins
	 * Based on current position of the player try to spawn randomly coins.
	 * Spawn time is random.
	 * Spawn position is aldo random, but with some restriction.
	 * * Coordinate of coin cannot be near other coins.
	 * * Coordinate of coin must be near the player.
	 * * Coordinate of coin cannot be overlap the ground.
	 * * The coins can spawn in front of the player, but if randomly the coordinate are back of the player reduce the distance.
	 * IEnumerator DeactivateDistanceCoins
	 * Each three seconds deactive the coins far from the player.
	 */

	[SerializeField] GameObject player;
	[SerializeField] Transform parent;
	[SerializeField] private GameObject coin;

	private GameObject[] coinsList;
	[SerializeField] private int maxCoinsOnScreen;

	int lastSpawnIndex = 0;

	void Start () {
		coinsList = new GameObject[maxCoinsOnScreen];
		for (int i = 0; i < maxCoinsOnScreen; i++) {
			coinsList[i] = GameObject.Instantiate(coin, new Vector2(0,0), Quaternion.identity, parent);
			coinsList[i].SetActive(false);
		}
		StartCoroutine("SpawnCoins");
		StartCoroutine("DeactivateDistanceCoins");
	}

	IEnumerator SpawnCoins() {
		while (true) {
			Vector2 futurePosition;
			Collider2D[] collider2Ds;
			bool nearAnotherCoin = false;
			bool noSpawn = false;

			yield return new WaitForSeconds(Random.Range(1f, 2.5f));

			int loops = 0;
			while(true){
				// Create random x, y coordinate. Start from current player position.
				// Clamp the coordinate in camera bounds range (with singleton)
				// Random.InitState((int)System.DateTime.Now.Ticks);  // Random seed
				futurePosition.x = Random.value >= 0.7 ? player.transform.position.x + Random.Range(0.3f, 3.0f) : player.transform.position.x - Random.Range(0.3f, 0.5f);
				futurePosition.x = Mathf.Clamp(futurePosition.x, CameraBoundsManager.instance.getLefTimit() - 2.2f, CameraBoundsManager.instance.getRightLimit() + 1);

				// Random.InitState((int)System.DateTime.Now.Ticks);
				futurePosition.y = player.transform.position.y + Random.Range(0.5f, 2.3f);
				futurePosition.y = Mathf.Clamp(futurePosition.y, CameraBoundsManager.instance.getBottomLimit() - 2, CameraBoundsManager.instance.getTopLimit() + 1);

				// Create Physics2D.OverlapCircleAll and search in the current new position we collide with other elements (like tile-map 'ground')
				collider2Ds = Physics2D.OverlapCircleAll(futurePosition, 0.2f);
				// for (int i = 0; i < collider2Ds.Length; i++) Debug.Log(collider2Ds[i].name);
				// Debug.Log("Spawn attempt at x: " + futurePosition.x + " y: " + futurePosition.x);
				if (collider2Ds.Length == 0){  // If the area it's free ...
					for (int i = 0; i < maxCoinsOnScreen; i++){
						if(coinsList[i] != null && coinsList[i].activeInHierarchy == true) {
							// Check if the new position have a distance greater than 0.5 with other 'active' coins
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
				// 10 Loops, but without collide OK, spanw.
				// 30 Loops, too much attempts, no spawn for the moment (like in subground)
				if (loops >= 10 && collider2Ds.Length == 0) break;
				else if(loops >= 30) {
					// Debug.Log("I can't spawn here!");
					noSpawn = true;
					break;
				}
				else loops += 1;
			}

			if (!noSpawn) {
				if (lastSpawnIndex >= maxCoinsOnScreen) lastSpawnIndex = 0;
				if (coinsList[lastSpawnIndex] != null && coinsList[lastSpawnIndex].activeInHierarchy == false) {
					// Debug.Log("Position and setActive at: " + lastSpawnIndex);
					coinsList[lastSpawnIndex].GetComponent<Coins>().Spawn(futurePosition, Quaternion.identity);
				}
				lastSpawnIndex += 1;
			}
		}
	}

	// If the coins have too much distance between the player setActive false and free the i-slots for a new transform position.
	IEnumerator DeactivateDistanceCoins(){
		while (true){
			yield return new WaitForSeconds(3);

			for (int i = 0; i < maxCoinsOnScreen; i++)
				if(coinsList[i] != null && Vector2.Distance(coinsList[i].transform.position, player.transform.position) >= 5)
					coinsList[i].SetActive(false);
		}
	}
}
