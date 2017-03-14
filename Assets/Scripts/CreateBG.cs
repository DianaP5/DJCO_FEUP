using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBG : MonoBehaviour {

   public GameObject background, newBackground;
    PlayerMovement player;

    public bool win;
    int count;
    Vector3 vec;
    // Use this for initialization
    void Start () {
        count = 0;
        background = GameObject.Find("FrontImage");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        vec.x = transform.position.x;
        vec.y = transform.position.y;
        vec.z = 95;
        
    }
	
	// Update is called once per frame
	void Update () {
        win = player.win;   
       
		if ( 47 + (count * 28) > background.transform.position.x && !win)
        {
            newBackground = Instantiate(background, vec, Quaternion.identity);
            background = newBackground;
            count++;
            vec.x = transform.position.x + (28 * count);
            vec.z = 95;
        }

    }
}
