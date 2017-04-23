using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Writer : MonoBehaviour {

	public string toWrite;
	public float characterDelay = 0.1f;

	private Text textArea;
	private bool finished;
	private bool ended;

	void Awake() {
		textArea = GetComponent<Text>();
		toWrite = textArea.text;
		textArea.text = "";
	}

	// Use this for initialization
	void Start () {
		StartWriting(toWrite);
	}
	
	// Update is called once per frame
	void Update () {
		if (finished && !ended) {
			Debug.Log("finished: " + finished);
			Debug.Log("ended: " + ended);
			EndOfWriting();
			ended = true;
		}
	}

	void StartWriting(string line) {
		StartCoroutine(IEWrite(line));
	}

	void EndOfWriting() {
		StartCoroutine(StartGame(1f));
	}

	IEnumerator StartGame(float delay) {
		yield return new WaitForSeconds(delay);
		Util.LoadScene("Main");
	}

	IEnumerator IEWrite(string line) {
		textArea.text = "";
		finished = false;
		int i = 0;
        for (i = 0; i < line.Length; i++) {
            yield return new WaitForSeconds(characterDelay);
            textArea.text += line[i];
			if (line.Length-1 == i) {
				finished = true;
			}
        }
	}
}
