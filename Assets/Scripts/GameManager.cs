using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private EnemySpawn[] enemySpawns;
	private Elevator[] elevators;
	public int level = 1;
	public int floor = 1;
	public int score = 0;

	public float initialTime;

	public Transform m_playerTransform;
	public PlayerController m_playerController;

	public bool hasUsedElevator;

	// Use this for initialization
	void Start () {

		enemySpawns = Object.FindObjectsOfType (typeof(EnemySpawn)) as EnemySpawn[];
		elevators = Object.FindObjectsOfType (typeof(Elevator)) as Elevator[];


		// Ignore collisions between ball and player
		Physics2D.IgnoreLayerCollision(9, 10, true);
		// Playr and enemy
		Physics2D.IgnoreLayerCollision(10, 11, true);

		initialTime = Time.timeSinceLevelLoad;

		hasUsedElevator = false;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (EnemySpawn spawn in enemySpawns) {
			if (!spawn.hasSpawned)
				spawn.Decide ();
		}
	}

	public void AddScore(int val) {
		score += val;
	}

	public void GoToFloor(int target) {

		if (hasUsedElevator)
			return;

		bool targetGoesUp = false;

		if (target > floor)
			targetGoesUp = false;
			// Find elevator
		else if (target < floor)
			targetGoesUp = true;
		else
			Debug.LogError ("Cant travel to the same floor");
		floor = target;

		foreach (Elevator elevator in elevators) {
			if (elevator.goesUp == targetGoesUp && (elevator.floor == target)) {
//				 Found correct elevator
				m_playerTransform.position = elevator.m_transform.position;
				hasUsedElevator = true;
			}
		}

		Debug.Log ("Traveled to floor: " + target.ToString ());
	}

	public void NextLevel() {
		Debug.Log ("Next level");
	}

	public void ElevatorStatus(bool status) {
		hasUsedElevator = status;
	}

}
