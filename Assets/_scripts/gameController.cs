using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	public int score;
	public float timer;

	public Transform spawnAlly;
	public Transform spawnEnemy;
	public Text scoreText;
	public GameObject zombie;
	public GameObject ally;



	// Use this for initialization
	void Start () {
		score = 0;
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		UpdateScore ();
		if (timer >= 20.0f) {
			Instantiate (zombie, spawnEnemy.position, spawnEnemy.rotation);
			Instantiate (ally, spawnAlly.position, spawnAlly.rotation);
			timer = 0.0f;
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

		
}
