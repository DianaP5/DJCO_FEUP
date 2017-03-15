using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;

	public PlayerMovement thePlayer;
	private Vector3 playerStartPoint;

	private PlatformDestroyer[] platformList;

	public DeathMenu theDeathScreen;

	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
	}

	// Update is called once per frame
	void Update () {

	}

	public void RestartGame()
	{
		thePlayer.gameObject.SetActive (false);
		theDeathScreen.gameObject.SetActive (true);
	}

	public void Reset()
	{
        theDeathScreen.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
    }
}
