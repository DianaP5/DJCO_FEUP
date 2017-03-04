using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 150f;
	public float timeBetweenJumps = 1 / 4f;

	public int curHealth;
	public int maxHealth;

	public bool grounded;
	public bool canDoubleJump;

	private float timestamp;

	private Animator anim;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("Grounded",grounded);
		anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));

		if (Input.GetAxis("Horizontal")<-0.1f && transform.localScale.x >0) {
			transform.localScale = new Vector3 (- transform.localScale.x, transform.localScale.y, 1);
		}

		if (Input.GetAxis("Horizontal")>0.1f ) {
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		}

		//jumping
		if (Time.time >= timestamp && Input.GetButton("Jump")) {
			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower);
				canDoubleJump = true;
			} else {
				if (canDoubleJump) {
					Debug.Log ("HERE");
					canDoubleJump = false;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower / 1.5f);
				}
			}
			timestamp = Time.time + timeBetweenJumps;
		}

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth<=0) {
			Die ();
		}
	}

	void FixedUpdate(){
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		float h = Input.GetAxis ("Horizontal");

		//Fake friction / easing the x speed of our player
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}

		rb2d.AddForce (Vector2.right * speed * h);

		//limit speed of player
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed) {
			rb2d.velocity = new Vector2 (Mathf.Sign(rb2d.velocity.x)* maxSpeed, rb2d.velocity.y);
		}
	}

	void Die(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
