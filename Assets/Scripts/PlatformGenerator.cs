using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject platform;
	public GameObject platCollider;
	public Transform generationPoint;
	public float distBetweenPlat;

	private float platformWidth;

	// Use this for initialization
	void Start () {
		platformWidth = platCollider.GetComponent<BoxCollider2D> ().size.x;
		Debug.Log (platformWidth);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {
			transform.position = new Vector3 (transform.position.x + platformWidth + distBetweenPlat, transform.position.y, transform.position.z);
			Instantiate (platform, transform.position, transform.rotation);
		}
	}
}
