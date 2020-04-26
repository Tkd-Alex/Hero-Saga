using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	[SerializeField] Text finalScoreText;
	void Start () {
		finalScoreText.text = PlayerStats.Points.ToString();
		ScoreManager.CreateEntry("Bingo-Bongo", PlayerStats.Points);
	}

}
