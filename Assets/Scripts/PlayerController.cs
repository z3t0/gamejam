using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D m_rigidBody2D;
	public Animator m_animator;
	public Transform m_transform;
	public Transform m_healthBar;
	public Transform m_staminaBar;

	public float health;
	public float stamina;
	public float walkSpeed;
	public float runSpeed;

	public float depleteStamina = 10.0f;
	public float gravity = -9.0f;

	bool flipped = false;

	// Use this for initialization
	void Start () {
		health = 100;
		stamina = 100;
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
			x = -1;
		}

		if (h > 0) {
			// Flip?
			Flip(false);
				
			m_animator.SetFloat("Speed", 0.3f);
			x = 1;
		}

		if (h == 0) {
			m_animator.SetFloat ("Speed", -0.1f);
		}

		if (Input.GetKey (KeyCode.LeftShift) && (h != 0)) {
			if (stamina > 10) {
				x = x * runSpeed;
				Tire (depleteStamina);
			} 

			else
				x = x * walkSpeed;
		}
			
		else
		{
			Rest (depleteStamina * 1.5f);
			x = x * walkSpeed;
		}


		m_rigidBody2D.velocity = new Vector2 (x, y + gravity);
	}

	// Action
	void Heal(float val)
	{
		if (health < 100) {
			if ((health + val) > 100)
				health = 100;
			else
				health += val;
		}

		SetBar (health, m_healthBar);
	}

	void Rest (float val) {
		if (stamina < 100) {
			if ((stamina + val) > 100)
				stamina = 100;
			else
				stamina += val;
		}
		SetBar (stamina, m_staminaBar);
	}

	void Hurt(float val) {
		health -= val;
		SetBar (health, m_healthBar);

		if (health <= 0) {
			Die ();
		}
	}

	void Tire(float val) {
		stamina -= val;
		SetBar (stamina, m_staminaBar);
	}

	void Die() {
		Debug.Log ("died");
	}

	void Flip (bool check) {
		if (flipped != check) {
			m_transform.localScale = new Vector3(m_transform.localScale.x * - 1, m_transform.localScale.y, m_transform.localScale.z);
			flipped = check;
		}
	}


	// UI
	void SetBar(float val, Transform bar) {
		val = val / 100;

		bar.localScale = new Vector3 (val, bar.localScale.y, bar.localScale.y);
	}
}
