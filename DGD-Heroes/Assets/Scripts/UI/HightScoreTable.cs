using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HightScoreTable : MonoBehaviour {
	[SerializeField] GameObject entryScorePrefab;
	[SerializeField] Transform startPosition;
	[SerializeField] float tableHeight = 0.70f;
	[SerializeField] float topN = 5;

	void Awake () {
		// Get the data from static class / PlayerPrefs
		// Then, save the list into List<> element. We need this because we can't serialize in PlayerPrefs directly the List<>
		ScoreManager.HightScores hightscores = ScoreManager.GetHightScores();
		List<ScoreManager.HightScoreEntry> hightscoreEntryList = hightscores.hightscoreEntryList;

		/*
		 * Iterate element one by one.
		 * Break the loop if we have more than topN element.
		 * Instantiate the prefab, setNameAndScore (inside UI.Text element) and Spanw (setActive(true), setPosition)
		 */
		for (int i=0; i < hightscoreEntryList.Count ; i++) {
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




