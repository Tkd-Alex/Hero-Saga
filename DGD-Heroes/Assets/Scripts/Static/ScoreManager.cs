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

	public static void CreateEntry(string name, int score) {
		HightScores hightscores = GetHightScores();
		HightScoreEntry entry = new HightScoreEntry { score = score, name = name };
		hightscores.hightscoreEntryList.Add(entry);
		
		string json = JsonUtility.ToJson(hightscores);
		PlayerPrefs.SetString("hightscoreTable", json);
		PlayerPrefs.Save();
	}

	public static HightScores GetHightScores() {
		HightScores hightscores;
		
		if (PlayerPrefs.HasKey("hightscoreTable")) {
			string jsonString = PlayerPrefs.GetString("hightscoreTable");
			hightscores = JsonUtility.FromJson<HightScores>(jsonString);
		} else hightscores = new ScoreManager.HightScores { hightscoreEntryList = new List<HightScoreEntry>() };

		return hightscores;
	}
}
