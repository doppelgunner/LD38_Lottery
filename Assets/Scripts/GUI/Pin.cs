using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pin : MonoBehaviour {

	public PersonType personType;
	public GameObject prefab;

	public void Add() {
		if (!GameManager.instance.GameOver && GameManager.instance.AddChosen(this)) {
			SoundManager.instance.PlayChoosePin(true);
			HidePin();
		} else {
			SoundManager.instance.PlayChoosePin(false);
		}
	}

	public void HidePin() {
		gameObject.SetActive(false);
	}

	public void ShowPin() {
		gameObject.SetActive(true);
	}
}
