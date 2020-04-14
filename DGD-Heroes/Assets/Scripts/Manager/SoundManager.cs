using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {
	public AudioClip clip;
	[Range(0f, 1f)] public float volume;
	// [Range(0f, 1f)] public float pitch;
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

		foreach(Sound s in sounds) {
			// Assign Sound class attribute to AudioSource unity type component.
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = 1;  // s.pitch;
			s.source.loop = s.loop;
		}
	}
	
	void Start() {
		Play("SoundTrack");
	}

	public void Play(string name) {
		for(int i=0; i < sounds.Length; i++) {
			if(sounds[i].name == name) {
				sounds[i].source.Play();
				break;
			}
		}
	}
}
