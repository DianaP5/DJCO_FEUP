using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private Player player;

	void Start(){
		player = gameObject.GetComponentInParent<Player> ();
	}

	void OnTriggerEnter2D (Collider2D coll){
		player.grounded = true;
	}

	void OnTriggerExit2D(Collider2D coll){
		player.grounded = false;
	}

	void OnTriggerStay2d(Collider2D coll){
		player.grounded = true;
	}

}
