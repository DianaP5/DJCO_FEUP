using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrollBackground : MonoBehaviour {


    PlayerMovement player;
    GameObject destructionPoint;
    public GameObject obj1;
    public float speed = 5f; 

    // Use this for initialization
    void Start () {

        destructionPoint = GameObject.Find("BackCollider");
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < destructionPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
        speed = player.moveSpeed;
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    
}
