using UnityEngine;
using System.Collections;

public class DeskWorker : MonoBehaviour {

	public static int pointsGained;
	public EnemySpawn m_spawn;
	public Rigidbody2D m_rigidBody2D;
	public BoxCollider2D m_collider;
	public GameManager m_gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Hit");
		if (other.gameObject.tag == "weapon") {
			Debug.Log ("Killed enemy");
			Die ();
		}
	}

	void Die() {
		m_spawn.hasSpawned = false;
		m_collider.enabled = false;

		Object.Destroy (gameObject, 3);
		m_gameManager.AddScore (pointsGained);
	}
}
