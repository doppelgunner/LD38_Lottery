using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public const string SEEN_INSTRUCTION = "SEEN_INSTRUCTION";

	public void LoadScene(string name) {
		SoundManager.instance.playSelect();
		SceneManager.LoadScene(name);
	}

	public void Reload() {
		SoundManager.instance.playSelect();
		LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Quit() {
		SoundManager.instance.playSelect();
		Application.Quit();
	}

	public void PlayGame() {
		int seen = PlayerPrefs.GetInt(SEEN_INSTRUCTION,0);
		if (seen!= 0) {
			LoadScene(GameManager.GAME_SCENE);
		} else {
			LoadScene(GameManager.INSTRUCTIONS_SCENE);
			PlayerPrefs.SetInt(SEEN_INSTRUCTION,1);
		}
	}

	public void ResetSeenInstruction() {
		PlayerPrefs.DeleteKey(SEEN_INSTRUCTION);
	}
}
