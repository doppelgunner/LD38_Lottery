using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpeechInsightType {
	GENERIC = 0,
	POSITIVE = 1,
	NEGATIVE = 2,
	GENERIC_POS = 3,
	GENERIC_NEG = 4,
}

[System.Serializable]
public class SpeechInsight {
	
	public PersonType personType;
	public string[] posSpeeches;
	public string[] negSpeeches;
}
