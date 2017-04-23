using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointType {
	NEUTRAL = 0,
	PEACE = 1, VIOLENCE = 2,
	MEANING = 3, FUN = 4,
	HUMANITY = 5, COMPASSION = 6,
}

public class Point : MonoBehaviour {

	[Range(-5,5)]
	public int kindness; //prisoner vs priest etc. -opp evil
	[Range(-5,5)]
	public int usefulness; //lawyer vs crazy example -opp weak
	[Range(-5,5)]
	public int hope; //prisoner vs priest example -opp pity

	public bool player; //this is for player

	public Point() {}

	public Point(int kindness, int usefulness, int hope, bool player) {
		this.kindness = kindness; //evil
		this.usefulness = usefulness;//weak
		this.hope = hope; //pity
		this.player = player;
	}

	//returns accumulated point
	public Point WinFrom(Point opponent) {

		int k = kindness - opponent.kindness;
		int u = usefulness - opponent.usefulness;
		int h = hope - opponent.hope;

		return new Point(k,u,h, player);
	}

	public void Add(Point other) {
		this.kindness += other.kindness;
		this.usefulness += other.usefulness;
		this.hope += other.hope;

		this.player = other.player;
	}

	public PointType GetKind() {
		if (kindness < 0) return PointType.VIOLENCE;
		else if (kindness > 0) return PointType.PEACE;
		return PointType.NEUTRAL;
	}

	public PointType GetUsefulness() {
		if (usefulness < 0) return PointType.FUN;
		else if (usefulness > 0) return PointType.MEANING;
		return PointType.NEUTRAL;
	}

	public PointType GetHope() {
		if (hope < 0) return PointType.COMPASSION;
		else if (hope > 0) return PointType.HUMANITY;
		return PointType.NEUTRAL;
	}

	//sign is not relevant for determining whether kind or bad etc...
	public PointType GetHighestPoint() {
		int k = Mathf.Abs(kindness);
		int u = Mathf.Abs(usefulness);
		int h = Mathf.Abs(hope);

		int[] arr = {k,u,h};
		int maxIndex = 0;
		int max = arr[0];
		for (int i = 1; i < 3; i++) {
			int result = Util.Compare(max,arr[i]);
			if (result < 0) {
				maxIndex = i;
				max = arr[i];
			}
		}

		if (maxIndex == 0) {
			return GetKind();
		} else if (maxIndex == 1) {
			return GetUsefulness();
		} else if (maxIndex == 2) {
			return GetHope();
		}

		return PointType.NEUTRAL;
	}

	public override string ToString() {
		return "kindness: " + kindness + ", usefulness: " + usefulness + ", hope: " + hope + ", player: " + player;
	}
}
