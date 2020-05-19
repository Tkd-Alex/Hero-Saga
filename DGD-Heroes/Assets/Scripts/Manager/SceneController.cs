using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public static SceneController instance;

	void Awake () {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

	public void LoadScene(string name) {
		SceneManager.LoadScene(name);
	}

	public void LoadScene(int index) {
		SceneManager.LoadScene(index);
	}
}
