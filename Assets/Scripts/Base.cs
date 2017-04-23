using UnityEngine;
using System.Collections;
using System;

public class Base : MonoBehaviour {
	
	private bool ready = false;
	public bool IsReady {
		get {
			return ready;
		}
	}

	public void LookAt(GameObject target) {
		Vector3 targetPosition = target.transform.position;
		targetPosition.y = transform.position.y;
		transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
	}

	public void ScaleIn(float startAfter, float speed, float endScale) {
		StartCoroutine(IEScaleIn(startAfter,speed,endScale));
	}

	public void ScaleBy(float startAfter, float speed, float scaleBy) {
		StartCoroutine(IEScaleBy(startAfter,speed,scaleBy));
	}

	public void ScaleOut(float startAfter, float speed) {
		StartCoroutine(IEScaleOut(startAfter, speed));
	}

	public void ScaleOut(float startAfter, float speed, Action callback) {
		StartCoroutine(IEScaleOut(startAfter, speed,callback));	
	}

	public IEnumerator IEScaleBy(float startAfter, float speed, float scaleBy) {
		yield return new WaitForSeconds(startAfter);

		float t = Time.deltaTime * speed;
		float scale = 1f;
		float diffScale = scaleBy - scale;
		Vector3 origScale = gameObject.transform.localScale;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			gameObject.transform.localScale = origScale * (scale + diffScale * Mathf.Sin(t));
			yield return null;
		}
		gameObject.transform.localScale = origScale * scaleBy;
	}

	public IEnumerator IEScaleIn(float startAfter, float speed, float endScale) {
	yield return new WaitForSeconds(startAfter);
		ready = false;
		float t = Time.deltaTime * speed;
		float scale = 1f;
		Vector3 origScale = gameObject.transform.localScale;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			scale = endScale * Mathf.Sin(t);
			gameObject.transform.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
		gameObject.transform.localScale = new Vector3(endScale, endScale, endScale);
		ready = true;
	}

	public IEnumerator IEScaleOut(float startAfter, float speed) {
		yield return IEScaleOutBase(startAfter, speed); 
		Destroy(gameObject);
	}

	public IEnumerator IEScaleOut(float startAfter, float speed, Action callback) {
		yield return IEScaleOutBase(startAfter,speed);
		callback();
		Destroy(gameObject);
	}

	IEnumerator IEScaleOutBase(float startAfter, float speed) {
		yield return new WaitForSeconds(startAfter);

		float t = Time.deltaTime * speed;
		float scale = 1f;
		Vector3 origScale = gameObject.transform.localScale;
		t = Mathf.PI / 2f;
		while(t < Mathf.PI) {
			t += Time.deltaTime * speed;
			scale = 1 * Mathf.Sin(t);
			gameObject.transform.localScale = origScale * scale;
			yield return null;
		}
	}
	
	
}
