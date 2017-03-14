using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonMenu : MonoBehaviour {

	public string mainMenuLevel;

    public GameObject wonMenu;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void QuitToMain()
	{
		Application.LoadLevel (mainMenuLevel);
	}
}
