using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject[] platformArray;
	public GameObject platform;
	public GameObject platCollider;

	public Transform generationPoint;

	public float[] distBetweenPlat;
	public float heigthBetweenPlat;
	public float minHeight;

	private float platformWidth;
	private float prevPlatformWidth;
	private int counter=0;

	private bool lastPlat;

	// Use this for initialization
	void Start () {
		prevPlatformWidth = platform.transform.FindChild("Collider").GetComponent<BoxCollider2D> ().size.x;
		lastPlat = false;
	}
	
	// Update is called once per frame
	void Update () {
		float HeightDist = Random.Range (-heigthBetweenPlat, heigthBetweenPlat);
		if (HeightDist>0) {
			HeightDist = HeightDist / 2;
		}
		if (transform.position.x < generationPoint.position.x) {
			int arrayPos;
			switch (arrayPos = Random.Range (0, 2)) {
			case 0:
				platform = platformArray [arrayPos];
				platformWidth = platform.transform.FindChild ("Collider").GetComponent<BoxCollider2D> ().size.x;
				transform.position = new Vector3 (transform.position.x + prevPlatformWidth + distBetweenPlat [arrayPos], transform.position.y + HeightDist < minHeight ? minHeight : transform.position.y + HeightDist, transform.position.z);
				break;
			case 1:
				platform = platformArray [arrayPos];
				platformWidth = platform.transform.FindChild ("Colliders").FindChild ("Bottom1").transform.localScale.x + platform.transform.FindChild ("Colliders").FindChild ("Bottom2").transform.localScale.x + platform.transform.FindChild ("Colliders").FindChild ("Spikes").transform.localScale.x;
				transform.position = new Vector3 (transform.position.x + prevPlatformWidth + distBetweenPlat [arrayPos], transform.position.y + HeightDist < minHeight ? minHeight : transform.position.y + HeightDist, transform.position.z);
				break;
			default:
				break;
			}
			Debug.Log (arrayPos);
			Instantiate (platform, transform.position, transform.rotation);
			prevPlatformWidth = platformWidth;
			//Debug.Log (transform.position.y);
			counter++;
		} else if (!lastPlat) {
			platform = platformArray [2];
			transform.position = new Vector3 (transform.position.x + prevPlatformWidth + 4, transform.position.y + HeightDist < minHeight ? minHeight : transform.position.y + HeightDist, transform.position.z);
			Instantiate (platform, transform.position, transform.rotation);
			lastPlat = true;
		}
		
	}
}
