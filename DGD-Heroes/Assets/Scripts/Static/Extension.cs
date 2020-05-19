using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension {

	// Extension of: Transform | Set coordinate and then setActive true.
	public static void Spawn(this Transform trans, Vector2 futurePosition, Quaternion rotation) {
		trans.SetPositionAndRotation(futurePosition, rotation);
		trans.gameObject.SetActive(true);
	}

	// Extension of: Transform | Change the localScale for 'flip' the GameObject, usually used for flip the enemy based on player.
	public static void Flip2D(this Transform trans, Transform otherTransform) {
		if (
			(otherTransform.position.x > trans.position.x && trans.localScale.x > 0) ||
			(otherTransform.position.x < trans.position.x && trans.localScale.x < 0)
		) trans.localScale = new Vector3(trans.localScale.x * -1, trans.localScale.y, trans.localScale.z);
	}

	// Extension of: Transform | Return a boolean value if other Transform element is near ...
	public static bool IsNearOther2D(this Transform trans, Transform otherTransform, float distance) {
		if (
			(Math.Abs(otherTransform.position.x - trans.position.x) >= 0.05) &&
			Vector2.Distance(trans.position, otherTransform.position) <= distance
		)
			return true;
		return false;
	}

	public static T ChangeAlpha<T>(this T g, float newAlpha) where T : Graphic {
		Color color = g.color;
		color.a = newAlpha;
		g.color = color;
		return g;
	}

	// Extension of: String | Convert the first char of a string in uppercase().
	public static string Capitalize(this String s) {
		if (string.IsNullOrEmpty(s)) return string.Empty;
		return char.ToUpper(s[0]) + s.Substring(1);
	}

	// Extension of: String | Repeat string n time, used in DotAnimation for repeat . .. ...
	public static string RepeatForLoop(this string s, int n) {
		var result = s;
		for (var i = 0; i < n - 1; i++) result += s;
		return result;
	}

	// Extension of: Transform | Find child Name, Score and then set UI text.
	public static void SetNameAndScore(this Transform trans, string name, int score) {
		trans.Find("PlayerName").GetComponent<Text>().text = name;
		trans.Find("PlayerScore").GetComponent<Text>().text = score.ToString();
	}

	public static void SortListByScore(this List<ScoreManager.HightScoreEntry> hightscoreEntryList) {
		// Just a little sorting method ... Best player on top.
		for (int i = 0; i < hightscoreEntryList.Count - 1; i++) {
			for (int j = i; j < hightscoreEntryList.Count; j++) {
				if (hightscoreEntryList[i].score < hightscoreEntryList[j].score) {
					ScoreManager.HightScoreEntry temp = hightscoreEntryList[i];
					hightscoreEntryList[i] = hightscoreEntryList[j];
					hightscoreEntryList[j] = temp;
				}
			}
		}
	}
}
