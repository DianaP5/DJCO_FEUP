using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

	private GameObject destructionPoint;

	void Start(){
		destructionPoint = GameObject.Find ("PlatformDestroyPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < destructionPoint.transform.position.x) {
			Destroy (gameObject);
		}
	}
}
