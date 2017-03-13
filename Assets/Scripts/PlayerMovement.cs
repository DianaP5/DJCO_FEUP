using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    //public float maxSpeed = 3;
    //public float speed = 50f;
    public float jumpPower = 150f;
    public float timeBetweenJumps = 1 / 4f;
    public float moveSpeed;

    public int curHealth;
    public int maxHealth;

    public int grounded;
    public bool canDoubleJump;

    public GameObject gameWonUI;
    public Text Livros;
    public Text scoreText;

    public CircleCollider2D magnet;
    public BoxCollider2D body;


    private float timestamp;

    private Animator anim;

    private Rigidbody2D rb2d;

    private PointEffector2D eff;

    private gameMaster gm;

    public GameObject booksound;
    public GameObject losesound;
    public GameObject winsound;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //eff = GetComponent<PointEffector2D>();
        eff = GetComponentInChildren<PointEffector2D>();
        eff.enabled = false;
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
        gameWonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool ("Grounded",grounded);
        anim.SetInteger("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

        /*
		if (Input.GetAxis("Horizontal")<-0.1f && transform.localScale.x >0) {
			transform.localScale = new Vector3 (- transform.localScale.x, transform.localScale.y, 1);
		}

		if (Input.GetAxis("Horizontal")>0.1f ) {
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		}*/

        //jumping
        if (Time.time >= timestamp && Input.GetButton("Jump"))
        {
            //if (grounded) {
            if (grounded == 1)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    Debug.Log("HERE");
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower / 1.5f);
                }
            }
            timestamp = Time.time + timeBetweenJumps;
        }

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        /*Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		//float h = Input.GetAxis ("Horizontal");

		//Fake friction / easing the x speed of our player
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}*/

        //rb2d.velocity = new Vector2 (moveSpeed, rb2d.velocity.y);

        //limit speed of player
        /*
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed) {
			rb2d.velocity = new Vector2 (Mathf.Sign(rb2d.velocity.x)* maxSpeed, rb2d.velocity.y);
		}*/
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator KnockBack(float knockDur, float knockPower, Vector3 kbDir)
    {
        float timer = 0;
        Debug.Log(knockDur);

        while (knockDur > timer)
        {
            Debug.Log("HERE2");
            timer += Time.deltaTime;

            rb2d.AddForce(new Vector3(kbDir.x * -75, kbDir.y * knockPower, transform.position.z));
        }
        yield return 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(body.IsTouching(col)){
            if (col.CompareTag("Book"))
            {
                booksound = GameObject.Find("Booksound");
                booksound.GetComponent<AudioSource>().Play();
                Destroy(col.gameObject);
                gm.points += 1;
            }
            if (col.CompareTag("Door"))
            {

                winsound = GameObject.Find("Winsound");
                winsound.GetComponent<AudioSource>().Play();

                if (gm.points <= 1)
                    Livros.text = ("You were able to collect " + gm.points + " Book!");
                else
                    Livros.text = ("You were able to collect " + gm.points + " Books!");

                if (gm.points < 10)
                {
                    scoreText.text = ("Exame score: F");
                }
                else if (gm.points < 20)
                {
                    scoreText.text = ("Exame score: D");
                }
                else if (gm.points < 30)
                {
                    scoreText.text = ("Exame score: C");
                }
                else if (gm.points < 40)
                {
                    scoreText.text = ("Exame score: B");
                }
                else if (gm.points < 50)
                {
                    scoreText.text = ("Exame score: A");
                }
                else if (gm.points >= 50)
                {
                    scoreText.text = ("Exame score: A+");
                }

                gameWonUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void EnableMagnet()
    {
        eff.enabled = true;
    }

    public void DisableMagnet()
    {
        eff.enabled = false;
    }
}
