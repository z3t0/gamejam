using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public EnemySpawn[] enemySpawns;
	public int level = 1;

	public int score = 0;

	public float initialTime;

	// Use this for initialization
	void Start () {
		foreach (EnemySpawn spawn in enemySpawns) {
			spawn.Decide ();
		}

		// Ignore collisions between ball and player
		Physics2D.IgnoreLayerCollision(9, 10, true);
		// Playr and enemy
		Physics2D.IgnoreLayerCollision(10, 11, true);

		initialTime = Time.timeSinceLevelLoad;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddScore(int val) {
		score += val;
	}

	public void NextLevel() {
		Debug.Log ("Next level");
	}

}
