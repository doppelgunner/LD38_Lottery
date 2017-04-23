using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PersonType {
	NONE = 0,
	DOCTOR = 1,
	PRIEST = 2,
	TEACHER = 3,
	PRISONER = 4,
	OTAKU = 5,
	CRAZY = 6,
	LAWYER = 7,
	PRESIDENT = 8,
	PLAYER = 9,
}

public class Person : Entity {

	public SpeechController speechController;
	public PersonType personType = PersonType.NONE;
	public PersonType opponent = PersonType.NONE; //TODO

	[Range(0,100)]
	public float negPercent = 33;
	[Range(0,100)]
	public float posPercent = 33;
	//remaining is generic

	public float scaleSpeed = 0.1f;
	public float fadeSpeed = 0.25f;
	public float speakEvery = 1f; //seconds
	public float speakPercent = 1f;

	public SpeechBank speechBank;
	public Range startDelayRange = new Range(0f,3f);

	private int opponentIndex;

	void Awake() {
		base.Awake();
		opponentIndex = speechBank.FindOpponentIndex(opponent);
	}

	// Use this for initialization
	void Start () {
		SayEverySec(speakEvery);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SayEverySec(float sec) {
		StartCoroutine(IESayEverySec(sec));
	}

	IEnumerator IESayEverySec(float sec) {
		while (true) {
			yield return new WaitForSeconds(sec);
			if (Util.CheckPercentRand(speakPercent)) {
				SaySomething();
			}
		}
	}

	public void SetOpponent(Person person) {
		SetOpponent(person.personType);
	}

	public void SetOpponent(PersonType opponent) {
		this.opponent = opponent;
		opponentIndex = speechBank.FindOpponentIndex(opponent);
		Debug.Log(opponentIndex);
	}

	public void SaySomething() {
		if (speechController == null) return;
		SpeechInsightType speechType = RandomSpeechType();
		string speech = speechBank.RandSpeech(opponentIndex,speechType);
		speechController.CreateSpeech(speech).Destroy(0,scaleSpeed,0,fadeSpeed);
	}

	SpeechInsightType RandomSpeechType() {
		float sum = negPercent + posPercent;
		float generic = 0;
		float max = 100;
		if (sum < 100) {
			generic = 100 - sum;
		} else {
			max = sum;
		}

		float rand = UnityEngine.Random.Range(1,max);
		if (rand <= negPercent) return SpeechInsightType.NEGATIVE;
		else if (rand > negPercent && rand <= sum) return SpeechInsightType.POSITIVE;
		else return SpeechInsightType.GENERIC;
	}

	public void SayLastWords() {
		if (speechController == null) return;
		string speech = speechBank.RandGoodByeSpeech();
		speechController.CreateSpeech(speech).Destroy(0,scaleSpeed,0,fadeSpeed);
	}

	public void Die(Action callback) {
		FadeOut(0,0.5f,callback);
		SayLastWords();
	}

	public Point GetWinPoint(Person opponent) {
		Point thisPoint = GetPoint();
		Point opponentPoint = opponent.GetPoint();
		return thisPoint.WinFrom(opponentPoint);
	}

	public Point GetPoint() {
		return GetComponent<Point>();
	}
}
