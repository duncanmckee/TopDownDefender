using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {

    public int highScore;
    public Text highScoreText;

	// Use this for initialization
	void Start () {
        highScore = SaveLoad.Load();
        highScoreText.text = "High Score:\n" + highScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        SaveLoad.Save(0);
        highScore = 0;
        highScoreText.text = "High Score:\n" + highScore;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
    }
}
