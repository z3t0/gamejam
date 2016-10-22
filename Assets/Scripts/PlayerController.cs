using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D m_rigidBody2D;
	public Animator m_animator;
	public Transform m_transform;

	public float walkSpeed;
	public float runSpeed;

	float gravity = -9.0f;

	bool flipped = false;

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
			Flip(true);

			m_animator.SetFloat ("Speed", 0.3f);
			x = -walkSpeed;
		}

		Debug.Log (h);

		if (h > 0) {
			// Flip?
			Flip(false);
				
			m_animator.SetFloat("Speed", 0.3f);
			x = walkSpeed;
		}

		if (h == 0) {
			m_animator.SetFloat ("Speed", -0.1f);
		}

		m_rigidBody2D.velocity = new Vector2 (x, y + gravity) * Time.deltaTime;
	}

	void Flip (bool check) {
		if (flipped != check) {
			m_transform.localScale = new Vector3(m_transform.localScale.x * - 1, m_transform.localScale.y, m_transform.localScale.z);
			flipped = check;
		}
	}
}
