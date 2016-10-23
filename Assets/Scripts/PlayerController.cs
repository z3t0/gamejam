using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D m_rigidBody2D;
	public Animator m_animator;
	public AudioSource m_audioSource;
	public BoxCollider2D m_collider;
	public Camera m_camera;
	public Transform m_transform;
	public Transform m_healthBar;
	public Transform ballSpawn;
	public Transform m_staminaBar;

	public LayerMask groundLayer;
	public LayerMask playerLayer;
	public LayerMask itemLayer;

	public GameObject ballPrefab;
	public AudioClip runSound;

	public float health;
	public float stamina;
	public float walkSpeed;
	public float runSpeed;
	public float jumpSpeed;

	public float shootX;
	public float shootY;

	public float depleteStamina = 10.0f;
	public float gravity = -9.0f;

	float distToGround;

	bool flipped = false;
	bool isGrounded;

	Vector3 shootPos;
	bool shooting;

	// Use this for initialization
	void Start () {
		health = 100;
		stamina = 100;
		m_audioSource.clip = runSound;

		// Ignore collisions between ball and player
		Physics2D.IgnoreLayerCollision(9, 10, true);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = m_camera.ScreenToWorldPoint (Input.mousePosition);
			if (!shooting) {
				Debug.Log ("shoot");
				m_animator.SetBool ("Shooting", true);
				shooting = true;
			}
		}
	}

	void FixedUpdate() {
		m_animator.SetBool ("Shooting", false);

		isGrounded = Physics2D.OverlapCircle(m_transform.position, 1.8f, groundLayer);

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		float x = 0;
		float y = 0;

		float threshold = 0.5f;

		if (h < -threshold) {
			// Flip
			Flip(true);

			m_animator.SetFloat ("Speed", 0.3f);
			x = -1;
		}

		if (h > threshold) {
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

		if (x != 0) {
			if(!m_audioSource.isPlaying)
				m_audioSource.Play ();
		} else {
			if(m_audioSource.isPlaying) 
				m_audioSource.Pause ();
		}

		if (Input.GetKey (KeyCode.Space) && isGrounded){
			y += jumpSpeed;
			Tire (15f);
		}


		m_rigidBody2D.velocity = new Vector2 (x, y + gravity);
	}

	// Action
	public void hasShot() {
		m_animator.SetBool ("Shooting", false);
		shooting = false;
	}
	public void Shoot() {
		if (!shooting) {
			GameObject ball = Instantiate (ballPrefab, ballSpawn) as GameObject;
			ball.transform.localPosition = Vector3.zero;
			if (flipped)
				ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-shootX, shootY), ForceMode2D.Impulse);
			else 
				ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootX, shootY), ForceMode2D.Impulse);
			shooting = true;
		}
	}

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
