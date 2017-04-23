using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardManager : MonoBehaviour {

	//useful
	//compassion
	//bad guys
	//
	public GameObject[] goldCoinPrefabs;
	public GameObject[] silverCoinPrefabs;
	public GameObject specialCoinPrefab;

	public GameObject _1stSpawnPoint;
	public GameObject coin1stSpawnPointsContainer;
	public GameObject coinOther1SpawnPointsContainer;
	public GameObject coinOther2SpawnPointsContainer;
	public GameObject specialCoinSpawn;

	private PointType _1st;
	private PointType other1;
	private PointType other2;
	private bool special;

	void Awake() {
		Compute();
		PopulateAward();
		GameManager.instance.Destroy();
	}

	void Start() {
		
	}

	GameObject GetGoldCoinPrefab(PointType pointType) {
		int n = (int)pointType;
		return goldCoinPrefabs[n];
	}

	GameObject GetSilverCoinPrefab(PointType pointType) {
		int n = (int)pointType;
		return silverCoinPrefabs[n];
	}

	void PopulateAward() {
		Util.ReplaceSpawnPoint(GetGoldCoinPrefab(_1st),_1stSpawnPoint);
	
		for (int i = 0; i < coin1stSpawnPointsContainer.transform.childCount; i++) {
			Util.ReplaceSpawnPoint(GetGoldCoinPrefab(_1st), coin1stSpawnPointsContainer.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < coinOther1SpawnPointsContainer.transform.childCount; i++) {
			Util.ReplaceSpawnPoint(GetSilverCoinPrefab(other1), coinOther1SpawnPointsContainer.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < coinOther2SpawnPointsContainer.transform.childCount; i++) {
			Util.ReplaceSpawnPoint(GetSilverCoinPrefab(other2), coinOther2SpawnPointsContainer.transform.GetChild(i).gameObject);
		}
		
		if (special) {
			Util.ReplaceSpawnPoint(specialCoinPrefab,specialCoinSpawn);
		}
	}

	

	void Compute() {
		Point point = null;
		if (GameManager.instance != null) {
			point = GameManager.instance.Point;
		}
/*
 #if UNITY_EDITOR
    	if (point == null) {
			point = new Point(-11,-1,-2,true);
		}
 #endif
 */

		_1st = point.GetHighestPoint();

		switch (_1st) {
			case PointType.PEACE:
			case PointType.VIOLENCE:
				other1 = point.GetHope();
				other2 = point.GetUsefulness();
				break;
			case PointType.MEANING:
			case PointType.FUN:
				other1 = point.GetHope();
				other2 = point.GetKind();
				break;
			case PointType.HUMANITY:
			case PointType.COMPASSION:
				other1 = point.GetUsefulness();
				other2 = point.GetKind();
				break;
		}

		special = !point.player;
	}
}
