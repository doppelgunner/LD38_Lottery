using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public AudioClip selectAudioClip;
	public AudioClip choosePinAudioClip;
	public AudioClip disabledPinAudioClip;
	public AudioClip pressDeathClip;
	public AudioClip killPersonClip;

	private AudioSource guiAudioSouce;
	private AudioSource personAudioSouce;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
		DontDestroyOnLoad(gameObject);

		guiAudioSouce = gameObject.AddComponent<AudioSource>();
		personAudioSouce = gameObject.AddComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playSelect() {
		guiAudioSouce.PlayOneShot(selectAudioClip,0.1f);
	}

	public void PlayChoosePin(bool enabled) {
		if (enabled) guiAudioSouce.PlayOneShot(choosePinAudioClip,0.1f);
		else guiAudioSouce.PlayOneShot(disabledPinAudioClip,0.05f);
	}

	public void PlayPressDeath() {
		guiAudioSouce.PlayOneShot(pressDeathClip,0.1f);
	}

	public void PlayKillPerson() {
		personAudioSouce.PlayOneShot(killPersonClip,0.1f);
	}


}
