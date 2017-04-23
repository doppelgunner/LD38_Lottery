using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour {

	public const float DEFAULT_WIDTH = 4.44f;
	public const float DEFAULT_HEIGHT = 2.21f;

	public float canvasWidth = DEFAULT_WIDTH;
	public float canvasHeight = DEFAULT_HEIGHT;

	//speech
	public const float DEFAULT_SPEECH_WIDTH = 4.44f;
	public const float DEFAULT_SPEECH_HEIGHT = 2.21f;
	public const float DEFAULT_SPEECH_SCALE = 0.005f;
	public const int DEFAULT_SPEECH_FONT_SIZE = 70;
	public const FontStyle DEFAULT_SPEECH_FONT_STYLE = FontStyle.Bold;
	//end speech

	public GameObject defaultSpeechObject;
	public Color speechColor = Color.white;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public Speech CreateSpeech(string speech) {
		GameObject speechObject = new GameObject("SpeechObject");
		speechObject.transform.parent = transform;

		Text speechText = speechObject.AddComponent<Text>();
		RectTransform speechRectTrans = speechObject.GetComponent<RectTransform>();

		speechRectTrans.localScale = new Vector3(DEFAULT_SPEECH_SCALE, DEFAULT_SPEECH_SCALE, DEFAULT_SPEECH_SCALE);
		speechRectTrans.sizeDelta = new Vector3(DEFAULT_SPEECH_WIDTH, DEFAULT_SPEECH_HEIGHT);
		speechText.fontSize = DEFAULT_SPEECH_FONT_SIZE;
		speechText.fontStyle = DEFAULT_SPEECH_FONT_STYLE;
		speechText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		speechText.horizontalOverflow = HorizontalWrapMode.Overflow;
		speechText.verticalOverflow = VerticalWrapMode.Overflow;
		speechText.alignment = TextAnchor.MiddleCenter;
		speechText.color = speechColor;

		Speech speechComp = speechObject.AddComponent<Speech>();
		speechComp.SetSpeech(speech);
		return speechComp;
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector2(canvasWidth,canvasHeight));
	}
}
