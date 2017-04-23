using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Util {

	public static bool CheckPercentRand(float percent) {
		float rand = Random.Range(0f,1f);
		if (rand <= percent) return true;
		return false;
	}

	public static void Shuffle<T>(List<T> list) {
		T newList;
		for (int i = 0; i < list.Count; i++) {
			newList = list[i];
			int randomIndex = Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = newList;
		}
	}

	// 0 - equal
	//>0 - num1 is greater
	//<0 - num2 is greater
	public static int Compare(int num1, int num2) {
		return num1 - num2;
	}

	public static GameObject ReplaceSpawnPoint(GameObject prefab, GameObject toReplace) {
		GameObject go =  MonoBehaviour.Instantiate(prefab,toReplace.transform.position, toReplace.transform.rotation);
		go.transform.localScale = toReplace.transform.localScale;
		MonoBehaviour.Destroy(toReplace);
		return go;
	}

	public static void LoadScene(string name) {
		SceneManager.LoadScene(name);
	}
}
