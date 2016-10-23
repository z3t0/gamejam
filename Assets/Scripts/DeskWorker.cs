using UnityEngine;
using System.Collections;

public class DeskWorker : MonoBehaviour {

	public int pointsGained;
	public EnemySpawn m_spawn;
	public Rigidbody2D m_rigidBody2D;
	public BoxCollider2D m_collider;
	public GameManager m_gameManager;
	public AudioSource m_audioSource;
	public AudioClip hurtSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "weapon") {
			Die ();
		}
	}

	void Die() {
		m_audioSource.clip = hurtSound;
		m_audioSource.Play ();
		m_spawn.Reset ();
		m_collider.enabled = false;

		Object.Destroy (gameObject, 3);
		m_gameManager.AddScore (pointsGained);
	}
}
