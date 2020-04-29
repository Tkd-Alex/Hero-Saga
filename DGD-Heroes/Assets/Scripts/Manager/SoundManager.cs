using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {
	public AudioClip clip;
	[Range(0f, 1f)] public float volume;
	public string name;
	public bool loop;
	[HideInInspector] public AudioSource source;
}

public class SoundManager : MonoBehaviour {
	public Sound[] sounds;
	public static SoundManager instance;
	void Awake () {
		if (instance == null) instance = this;
		else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		/*
		 * Create an array of Sound element.
		 * Each element have a AudioSource component and other attribute.
		 * Create custom Serializable class just for have custom audio settings easy accesibile by name from unity editor
		*/
		foreach(Sound s in sounds) {
			// Assign Sound class attribute to AudioSource unity type component.
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = 1;
			s.source.loop = s.loop;
		}
	}

	void Start() {
		Play("SoundTrack");  // Obviously on start play the Soundtrack
	}

	/*
	 * Search the sounds by name
	 * Play the sound
	 */
	public void Play(string name) {
		for(int i=0; i < sounds.Length; i++) {
			if(sounds[i].name == name) {
				sounds[i].source.Play();
				break;
			}
		}
	}
}
