using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameManager m_gameManager;

	public GameObject deskPersonPrefab;
	public GameObject workerPrefab;
	public GameObject bossPrefab;

	public Transform m_transform;

	public bool hasSpawned = false;


	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Decide() {
		if (hasSpawned)
			return;
		
		int chance = Random.Range (1, 100);
		int difficulty = m_gameManager.level ;

		// Boss
//		if (chance <= (5 * difficulty)) {
//			spawnBoss ();
//		}

//		else if (chance <= 30) {
			spawnDeskPerson ();
//		} 
//			
//		else if (chance <= 75) { // probability of 25%
//			spawnWorker ();
//		} 

	}

	void spawnBoss() {

	}

	void spawnWorker() {

	}

	void spawnDeskPerson() {
		hasSpawned = true;
		GameObject go = Instantiate (deskPersonPrefab, m_transform) as GameObject;
		DeskWorker worker = go.GetComponent<DeskWorker> ();

		worker.m_spawn = this;
		worker.m_gameManager = m_gameManager;
	}

}
