using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillButton : MonoBehaviour {


	private Button button;

	void Awake() {
		button = GetComponent<Button>();
	}

	public void Disable() {
		button.interactable = false;
	}

	public void Enable() {
		button.interactable = true;
	}

	public void PressLeft() {
		SoundManager.instance.PlayPressDeath();
		if (GameManager.instance.killLeft()) {
			Disable();
		}
	}

	public void PressRight() {
		SoundManager.instance.PlayPressDeath();
		if (GameManager.instance.killRight()) {
			Disable();
		}
	}
}
