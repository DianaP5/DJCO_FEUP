using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	public int spikeDmg;

	private PlayerMovement player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag("Player")) {
			player.Damage (spikeDmg); 

			//StartCoroutine (player.KnockBack (0.02f, 55, player.transform.position));
		}
	}
}
