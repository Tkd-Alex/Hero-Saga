using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager{

	// Cannot store a List into a json PlayerPrefs (why ?) Anyway create a class for entryList
	public class HightScores {
		public List<HightScoreEntry> hightscoreEntryList;
	}

	[System.Serializable]  // Important for JSON conversion
	public class HightScoreEntry {
		public int score;
		public string name;
	}

	/*
	 * Get current HightScores value
	 * Create a new entry with name and score values
	 * Append the item to current List<>
	 * Convert the class HightScores to JSON and Save the PlayerPrefs
	 */
	public static void CreateEntry(string name, int score, int topN=10) {
		HightScores hightscores = GetHightScores();
		HightScoreEntry entry = new HightScoreEntry { score = score, name = name };
		hightscores.hightscoreEntryList.Add(entry);

		hightscores.hightscoreEntryList.SortListByScore();

		HightScores reducedScores = new HightScores { hightscoreEntryList = new List<HightScoreEntry>() };
		for (int i = 0; i < hightscores.hightscoreEntryList.Count; i++) {
			if (i >= topN) break;
			reducedScores.hightscoreEntryList.Add(hightscores.hightscoreEntryList[i]);
		}

		string json = JsonUtility.ToJson(reducedScores);
		PlayerPrefs.SetString("hightscoreTable", json);
		PlayerPrefs.Save();
	}

	/*
	 * If PlayerPrefs has key hightscoreTable
	 * Then, get data from PlayerPrefs and convert to json, then cast to HightScores
	 * Else create empty values ...
	 */
	public static HightScores GetHightScores() {
		HightScores hightscores;

		if (PlayerPrefs.HasKey("hightscoreTable")) {
			string jsonString = PlayerPrefs.GetString("hightscoreTable");
			hightscores = JsonUtility.FromJson<HightScores>(jsonString);
		} else hightscores = new HightScores { hightscoreEntryList = new List<HightScoreEntry>() };

		hightscores.hightscoreEntryList.SortListByScore();
		return hightscores;
	}
}
