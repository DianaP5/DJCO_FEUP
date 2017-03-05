using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject platform;
	public GameObject platCollider;

	public Transform generationPoint;

	public float distBetweenPlat;
	public float heigthBetweenPlat;
	public float minHeight;

	private float platformWidth;

	// Use this for initialization
	void Start () {
		platformWidth = platCollider.GetComponent<BoxCollider2D> ().size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {
			float HeightDist = Random.Range (-heigthBetweenPlat, heigthBetweenPlat);
			transform.position = new Vector3 (transform.position.x + platformWidth + distBetweenPlat, transform.position.y+HeightDist < minHeight ? minHeight : transform.position.y+HeightDist , transform.position.z);
			Instantiate (platform, transform.position, transform.rotation);
			Debug.Log (transform.position.y);
		}
		
	}
}
