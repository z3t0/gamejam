using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D m_rigidBody2D;
	public Animator m_animator;

	public float walkSpeed;
	public float runSpeed;

	float gravity = -9.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		float x = 0;
		float y = 0;

		if (h < 0) {
			// Flip
		}

		if (h > 0) {
			// Flip?
			m_animator.SetFloat("Speed", 0.3f);
			x = walkSpeed;
		}



		m_rigidBody2D.velocity = new Vector2 (x, y + gravity) * Time.deltaTime;
	}
}
