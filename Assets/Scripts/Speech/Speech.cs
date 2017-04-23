using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Speech : Base {
	public static readonly Range xRange = new Range(-2.35f,2.35f);
	public static readonly Range yRange = new Range(1.0f,2.0f);

	private float x;
	private float y;

	public string speech;
	private Text speechText;
	private RectTransform speechRectTrans;

	public void SetSpeech(string speech) {
		speechText.text = this.speech = speech;
	}

	void Awake() {
		speechText = GetComponent<Text>();
		speechRectTrans = GetComponent<RectTransform>();
	}

	// Use this for initialization
	void Start () {
		x = xRange.GetRand();
		y = yRange.GetRand();

		transform.localPosition = new Vector2(x,y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector2(SpeechController.DEFAULT_WIDTH,SpeechController.DEFAULT_HEIGHT));
	}

	public void Destroy(float scaleAfter, float scaleSpeed, float fadeAfter, float fadeSpeed) {
		ScaleOut(scaleAfter,scaleSpeed);
		FadeOut(fadeAfter,fadeSpeed);
		//ScaleOut(0,0.1f);
		//FadeOut(0,0.25f);
	}

	public void FadeOut(float startAfter, float speed) {
		StartCoroutine(IEFadeOut(startAfter, speed));
	}

	public void FadeOut(float startAfter, float speed, Action callback) {
		StartCoroutine(IEFadeOut(startAfter, speed,callback));	
	}

	IEnumerator IEFadeOut(float startAfter, float speed) {
		yield return IEFadeOutBase(startAfter, speed); 
		Destroy(gameObject);
	}

	IEnumerator IEFadeOut(float startAfter, float speed, Action callback) {
		yield return IEFadeOutBase(startAfter,speed);
		callback();
		Destroy(gameObject);
	}

	IEnumerator IEFadeOutBase(float startAfter, float speed) {
		yield return new WaitForSeconds(startAfter);

		float scale = 1f;
		Color origColor = speechText.color;
		float origAlpha = origColor.a;
		float t = Mathf.PI / 2f;
		while (t < Mathf.PI) {
			t += Time.deltaTime * speed;
			scale = 1 * Mathf.Sin(t);
			speechText.color = new Color(origColor.r,origColor.g,origColor.b,scale * origAlpha);
			yield return null;
		}
	}
}
