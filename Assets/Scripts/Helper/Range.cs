using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Range {

	public float min;
	public float max;

	public Range(float min, float max) {
		this.min = min;
		this.max = max;
	}

	public float GetRand() {
		return Random.Range(min,max);
	}

	public override string ToString() {
		return min + " <-> " + max;
	}
}
