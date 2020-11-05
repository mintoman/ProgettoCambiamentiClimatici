using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class MenuLogic2D : MonoBehaviour
{
    public static MenuLogic2D Instance;

    public GameObject startScreenPanel;

    public GameObject inGameScreenPanel;

    public GameObject gameOverScreenPanel;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI gameOverScore;

    void Awake()
    {
        Instance = this;
        //GooglePlayServicesManager.Instance.TrySilentSignIn();
    }

    public void MenuShow()
    {
        //GooglePlayServicesManager.Instance.SignIn();

        this.startScreenPanel.SetActive(true);
        this.inGameScreenPanel.SetActive(false);
        this.gameOverScreenPanel.SetActive(false);
    }


    public void GameShow()
    {
        this.startScreenPanel.SetActive(false);
        this.inGameScreenPanel.SetActive(true);
        this.gameOverScreenPanel.SetActive(false);

        //this.GameUpdate ();
    }

    public void GameOverShow()
    {
        this.startScreenPanel.SetActive(false);
        this.inGameScreenPanel.SetActive(false);
        this.gameOverScreenPanel.SetActive(true);

        //this.GameUpdate ();
    }


    public void SetGameOverScore(int score)
    {

        TimeSpan result = TimeSpan.FromMinutes(score);

        string fromTimeString = result.ToString("hh':'mm");

        //gameOverScore.SetText("{0}", score);
        gameOverScore.SetText(fromTimeString, score);
    }

    /*
	public void PressMenuButtonPlay ()
	{
		MusicManager.Instance.PlayMenuButtonPressed ();
		
		GameLogic2D.Instance.GameStart ();
	}
	
	public void PressGameOverButtonRestart ()
	{
		MusicManager.Instance.PlayMenuButtonPressed ();
		
		GameLogic2D.Instance.GameRestart ();
	}*/
}
