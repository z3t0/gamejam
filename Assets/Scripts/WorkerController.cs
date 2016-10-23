using UnityEngine;
using System.Collections;

public class WorkerController : MonoBehaviour {

	public int pointsGained;

	public Transform m_transform;
	public LayerMask playerLayer;
	public Animator  m_animator;
	public AudioSource m_audioSource;
	public BoxCollider2D m_collider;
	public Rigidbody2D m_rigidBody2D;
	public GameManager m_gameManager;
	public EnemySpawn m_spawn;

	public AudioClip hurtSound;

	public bool sensed;

	float direction = 1;
	float countDownLength = 3.0f;
	float countDown = 0.0f;

	public float walkSpeed = 5.0f;
	public float runSpeed = 15.0f;

	// Use this for initialization
	void Start () {
		sensed = false;
	}
	
	// Update is called once per frame
	void Update () {
		sensed = Physics2D.OverlapCircle(m_transform.position, 5f, playerLayer);

		if (sensed) {

			m_animator.SetFloat ("Speed", 0.7f);

			float x = m_gameManager.m_playerTransform.position.x;

			if (x > m_transform.position.x) {
				direction = -1f;
			}
			else if (x < m_transform.position.x) {
				direction = 1f;
			}
			m_rigidBody2D.velocity = new Vector2 (runSpeed * direction, -9f);
			m_transform.localScale = new Vector3 (m_transform.localScale.x * -1, m_transform.localScale.y, m_transform.localScale.z);



		} else if (!sensed) {
			m_animator.SetFloat ("Speed", 0.3f);
			m_rigidBody2D.velocity = new Vector2 (walkSpeed * direction, -9f);
		

			if (countDown <= 0.0f) {
				Flip ();
				countDown = countDownLength;
			} else {
				countDown -= 1 * Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Hit");
		if (other.gameObject.tag == "weapon") {
			Debug.Log ("Killed enemy");
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

	void Flip() {
		direction *= -1;
		m_rigidBody2D.velocity = new Vector2 (m_rigidBody2D.velocity.x *  direction, -9f);
		m_transform.localScale = new Vector3 (m_transform.localScale.x * -1, m_transform.localScale.y, m_transform.localScale.z);
	}
}
