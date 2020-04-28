using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HightScoreTable : MonoBehaviour {
	[SerializeField] GameObject entryScorePrefab;
	[SerializeField] Transform startPosition;
	[SerializeField] float tableHeight = 0.70f;
	[SerializeField] float topN = 5;

	void Awake () {
		ScoreManager.HightScores hightscores = ScoreManager.GetHightScores();
		List<ScoreManager.HightScoreEntry> hightscoreEntryList = hightscores.hightscoreEntryList;

		// Bubble-Sort :D | https://www.tutorialspoint.com/Bubble-Sort-program-in-Chash
		for (int j = 0; j <= hightscoreEntryList.Count - 2; j++) {
			for (int i = 0; i <= hightscoreEntryList.Count - 2; i++) {
				if (hightscoreEntryList[i].score < hightscoreEntryList[i + 1].score) {
					ScoreManager.HightScoreEntry temp = hightscoreEntryList[i + 1];
					hightscoreEntryList[i + 1] = hightscoreEntryList[i];
					hightscoreEntryList[i] = temp;
				}
			}
		}

		for (int i=0; i < topN; i++) {
			if (i >= topN) break;
			GameObject entry = Instantiate(entryScorePrefab, startPosition);
			entry.transform.SetNameAndScore(hightscoreEntryList[i].name, hightscoreEntryList[i].score);
			entry.transform.Spawn(new Vector2(startPosition.position.x, startPosition.position.y - tableHeight * i), startPosition.rotation);
		}
	}

	public void BackToMenu() {
		SceneController.instance.LoadScene("Menu");
	}
}




