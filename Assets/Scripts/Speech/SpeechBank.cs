using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeechBank {

	public SpeechInsight[] speechInsights;
	public string[] genericSpeeches = {
		"Save me",
		"Please!!!",
		"Please!",
		"...",
	};

	public string[] goodByeSpeeches = {
		"Noooo!",
		"Fuck...",
		"Huhh",
	};

	public string RandGoodByeSpeech() {
		int rand = Random.Range(0,goodByeSpeeches.Length);
		return goodByeSpeeches[rand];
	}

	public string RandSpeech(int opponentIndex, SpeechInsightType speechType) {
		string[] arr = null;
		if (opponentIndex < 0) {
			arr = genericSpeeches;
		} else {
			switch (speechType) {
				case SpeechInsightType.GENERIC:
					arr = genericSpeeches;
					break;
				case SpeechInsightType.NEGATIVE:
					arr = speechInsights[opponentIndex].negSpeeches;
					break;
				case SpeechInsightType.POSITIVE:
					arr = speechInsights[opponentIndex].posSpeeches;
					break;
			}
		}

		if (arr.Length == 0) {
			arr = genericSpeeches;
		}
		if (arr == null) return null;
		int index = RandFromArray(arr);
		return arr[index];
	}

	public int RandFromArray(string[] arr) {
		int length = arr.Length;
		return Random.Range(0,length);
	}

	public int FindOpponentIndex(PersonType opponent) {
		for (int i = 0; i < speechInsights.Length; i++) {
			if (speechInsights[i].personType == opponent) {
				return i;
			}
		}
		return -1;
	}
}
