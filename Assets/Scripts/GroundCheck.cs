using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private PlayerMovement player;

	void Start(){
		player = gameObject.GetComponentInParent<PlayerMovement> ();
		player.grounded = 0;
	}

	void OnTriggerEnter2D (Collider2D coll){
		if (!coll.CompareTag("Book")) {
			player.grounded++;
		}

	}

	void OnTriggerExit2D(Collider2D coll){
		if (!coll.CompareTag("Book")) {
			player.grounded--;
		}
	}
    

}
