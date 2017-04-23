using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public const string GAME_SCENE = "Main";
	public const string MENU_SCENE = "Menu";
	public const string VERDICT_SCENE = "Verdict";
	public const string INSTRUCTIONS_SCENE = "Instructions";

	public static GameManager instance;
	public List<GameObject> prefabs;
	public GameObject playerPrefab;

	public KillButton leftButton;
	public KillButton rightButton;
	
	public List<GameObject> spawnPoints;

	private List<Pin> chosen;
	private List<Person> battling;

	private bool picked;
	private bool ready;
	private int peopleCounter = 8;
	private bool lastLevel;

	private Point point = new Point();

	public Point Point {
		get {return point;}
	}

	public bool GameOver {
		get {return lastLevel;}
	}

	public GameManager() {
		chosen = new List<Pin>();
		battling = new List<Person>();
	}

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		//Util.Shuffle(prefabs);
		//Spawn();
		leftButton.Enable();
		rightButton.Enable();
	}
	
	// Update is called once per frame
	void Update () {
/*
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.A)) {
			killLeft();
		} else if (Input.GetKeyDown(KeyCode.S)) {
			killRight();
		}
#endif
*/
	}

	public bool AddChosen(Pin pin) {
		if (chosen.Count >= 2) return false;
		chosen.Add(pin);
		if (chosen.Count == 2) {
			Spawn(chosen[0].prefab,chosen[1].prefab);
		}
		if (peopleCounter == 1) {
			Spawn(chosen[0].prefab,playerPrefab);	
			lastLevel = true;
		}
		return true;
	}

	public void Spawn(GameObject g1, GameObject g2) {
		int index = 0;
		GameObject one = Instantiate(g1,spawnPoints[index].transform.position, Quaternion.identity);
		Person p1 = one.GetComponent<Person>();
		index++;
		GameObject two = Instantiate(g2,spawnPoints[index].transform.position, Quaternion.identity);
		Person p2 = two.GetComponent<Person>();
		one.transform.localScale = Vector3.zero;
		two.transform.localScale = Vector3.zero;

		p1.ScaleIn(0,0.5f,1);
		p1.FadeIn(0,0.5f,SetReady);
		p2.ScaleIn(0,0.5f,1);
		p2.FadeIn(0,0.5f,SetReady);

		p1.SetOpponent(p2);
		p2.SetOpponent(p1);

		battling.Add(p1);
		battling.Add(p2);
	}

	void SetReady() {
		ready = true;
	}
/*
	public void Spawn() {
		if (prefabs.Count <= 0) return;
		if (prefabs.Count == 1) {
			return;
		}
		
		//else >= 2
		int index = 0;
		GameObject one = Instantiate(prefabs[index],spawnPoints[index].transform.position, Quaternion.identity);
		Person p1 = one.GetComponent<Person>();
		index++;
		GameObject two = Instantiate(prefabs[index],spawnPoints[index].transform.position, Quaternion.identity);
		Person p2 = two.GetComponent<Person>();
	}
	*/

	public void Reload() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public bool killLeft() {
		if (picked || !ready) return false;
		SoundManager.instance.PlayKillPerson();

		picked = true;
		battling[0].Die(EndLevel);
		if (peopleCounter > 1) {
			chosen[1].ShowPin();
		} 
		point.Add(battling[1].GetWinPoint(battling[0]));
		//point.Add(battling[1].GetPoint());

		peopleCounter--; //improve
		return true;
	}

	public bool killRight() {
		if (picked || !ready) return false;
		SoundManager.instance.PlayKillPerson();

		picked = true;
		battling[1].Die(EndLevel);
		chosen[0].ShowPin();
		
		point.Add(battling[0].GetWinPoint(battling[1]));
		//point.Add(battling[0].GetPoint());

		peopleCounter--; //improve
		return true;
	}

	void EndLevel() {
		Reset();
		if (!lastLevel) {
			Reload();
			leftButton.Enable();
			rightButton.Enable();
		} else {
			Util.LoadScene("Verdict");
			Debug.Log(point); //TODO
		}
	}

	void Reset() {
		picked = false;
		ready = false;
		chosen.Clear();
		battling.Clear();
	}

	public void Destroy() {
		Destroy(gameObject);
	}
}
