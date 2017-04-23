using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : Base {

	protected SpriteRenderer spriteRenderer;

	protected void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		if (callback != null) {
			callback();
		}
		Destroy(gameObject);
	}

	IEnumerator IEFadeOutBase(float startAfter, float speed) {
		yield return new WaitForSeconds(startAfter);

		float scale = 1f;
		Color origColor = spriteRenderer.color;
		float origAlpha = origColor.a;
		float t = Mathf.PI / 2f;
		while (t < Mathf.PI) {
			t += Time.deltaTime * speed;
			scale = 1 * Mathf.Sin(t);
			spriteRenderer.color = new Color(origColor.r,origColor.g,origColor.b,scale * origAlpha);
			yield return null;
		}
	}

	public void FadeIn(float startAfter, float speed) {
		FadeIn(startAfter, speed,null);
	}
	
	public void FadeIn(float startAfter, float speed, Action callback) {
		StartCoroutine(IEFadeIn(startAfter,speed,callback));
	}

	IEnumerator IEFadeIn(float startAfter, float speed, Action callback) {
		yield return IEFadeIn(startAfter, speed);
		if (callback != null) {
			callback();
		}
	}

	IEnumerator IEFadeIn(float startAfter, float speed) {
	yield return new WaitForSeconds(startAfter);
		float t = Time.deltaTime * speed;
		float scale = 1f;
		Color origColor = spriteRenderer.color;
		float endAlpha = origColor.a;
		Vector3 origScale = gameObject.transform.localScale;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			scale = endAlpha * Mathf.Sin(t);
			spriteRenderer.color = new Color(origColor.r,origColor.g,origColor.b,scale);
			yield return null;
		}
		spriteRenderer.color = new Color(origColor.r,origColor.g,origColor.b,endAlpha);
	}
}
