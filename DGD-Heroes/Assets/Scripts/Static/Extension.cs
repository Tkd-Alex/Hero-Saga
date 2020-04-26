using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension {

	public static void Spawn(this Transform trans, Vector2 futurePosition, Quaternion rotation) {
		trans.SetPositionAndRotation(futurePosition, rotation);
		trans.gameObject.SetActive(true);
	}

	public static void Flip2D(this Transform trans, Transform otherTransform) {
		if (
			(otherTransform.position.x > trans.position.x && trans.localScale.x > 0) ||
			(otherTransform.position.x < trans.position.x && trans.localScale.x < 0)
		) trans.localScale = new Vector3(trans.localScale.x * -1, trans.localScale.y, trans.localScale.z);
	}

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

	public static string Capitalize(this String s) {
		if (string.IsNullOrEmpty(s)) return string.Empty;
		return char.ToUpper(s[0]) + s.Substring(1);
	}

	public static void SetNameAndScore(this Transform trans, string name, int score) {
		trans.Find("PlayerName").GetComponent<Text>().text = name;
		trans.Find("PlayerScore").GetComponent<Text>().text = score.ToString();
	}

}
