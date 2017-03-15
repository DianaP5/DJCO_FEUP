using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{    
    public float jumpPower = 150f;
    public float timeBetweenJumps = 1 / 4f;
    public float moveSpeed;

    public int curHealth;
    public int maxHealth;

    public int grounded;
    public bool canDoubleJump;

    public GameObject gameWonUI;
    public GameObject gameLostUI;
    public Text Livros;
    public Text scoreText;
    public Text currentScore;

    public CircleCollider2D magnet;
    public BoxCollider2D body;


    private float timestamp;

    private Animator anim;

    private Rigidbody2D rb2d;

    private PointEffector2D eff;

    private gameMaster gm;

    public GameObject themeSound;
    public GameObject bookSound;
    public GameObject loseSound;
    public GameObject winSound;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        eff = GetComponent<PointEffector2D>();
        //eff = GetComponentInChildren<PointEffector2D>();
        eff.enabled = false;
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
        gameWonUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

        //jumping
        if (Time.time >= timestamp && Input.GetButton("Jump"))
        {
            if (grounded > 0)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
                //grounded = 0;
            }
            else
            {
                if (canDoubleJump)
                {
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

        if (rb2d.position.y < -25)
        {
            Die();
        }
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }

    void Die()
    {
        //Time.timeScale = 0f;
        moveSpeed = 0;
        themeSound = GameObject.Find("Theme");
        themeSound.GetComponent<AudioSource>().Stop();
        gameLostUI.SetActive(true);
    }

    public IEnumerator KnockBack(float knockDur, float knockPower, Vector3 kbDir)
    {
        float timer = 0;

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
                bookSound = GameObject.Find("Booksound");
                bookSound.GetComponent<AudioSource>().Play();
                Destroy(col.gameObject);
                gm.points += 1;
                currentScore.text = "x " + gm.points.ToString();
            }
            //not in use
            if (col.CompareTag("Door"))
            {
                themeSound = GameObject.Find("Theme");
                themeSound.GetComponent<AudioSource>().Stop();
                winSound = GameObject.Find("Winsound");
                winSound.GetComponent<AudioSource>().Play();

                if (gm.points == 1)
                    Livros.text = ("You were able to collect 1 Book!");
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
