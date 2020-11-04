
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public BoardManager boardManager;

    public enum STATE
    {
        //None,
        Menu,
        Play,
        GameOver
    }
    public STATE state;

    void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start()
    {
        this.state = STATE.Menu;

        // SHOW START MENU
        MenuLogic2D.Instance.MenuShow();
    }

    public void StartGame()
    {
        if (this.state == STATE.Menu)
        {
            this.state = STATE.Play;
            MenuLogic2D.Instance.GameShow();
            boardManager.SetupBoard();
        }   
    }

    public void OnGameOver()
    {
        if (this.state == STATE.Play)
        {
            this.state = STATE.GameOver;
            MenuLogic2D.Instance.GameOverShow();
        }
    }

    public void Restart()
    {
        if (this.state == STATE.GameOver)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
