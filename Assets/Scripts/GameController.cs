using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    //Tile Manager
    public TileManager tileManager;

    public bool b_GameStarted;

    public GameObject difficultyCanvas;
    public GameObject startCanvas;
    public GameObject gameUiCanvas;
    public GameObject gameOverUI;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI movesLeftNum;

    public TextMeshProUGUI finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        b_GameStarted = false;

        ShowDifficultyUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !b_GameStarted)
        {
            StartGame();
            b_GameStarted = true;
        }

        int time = (int)tileManager.GetRemainingTime();
        timeText.text = time.ToString();

        scoreText.text = tileManager.GetScore().ToString();
        movesLeftNum.text = tileManager.GetNumberOfMoves().ToString();

        if (tileManager.gameOver == true)
        {
            ShowGameOverUi();
        }

    }

    private void StartGame()
    {
        Debug.Log("Game Started");
        ShowGameUI();
        b_GameStarted = true;
        tileManager.GenerateEmptyGrid(11, 11);
    }


    public void ShowDifficultyUI()
    {
        difficultyCanvas.SetActive(true);
        startCanvas.SetActive(false);
        gameUiCanvas.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void ShowStartUI()
    {
        difficultyCanvas.SetActive(false);
        startCanvas.SetActive(true);
        gameUiCanvas.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void ShowGameUI()
    {
        difficultyCanvas.SetActive(false);
        startCanvas.SetActive(false);
        gameUiCanvas.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void ShowGameOverUi()
    {
        tileManager.enabled = false;

        difficultyCanvas.SetActive(false);
        startCanvas.SetActive(false);
        gameUiCanvas.SetActive(false);
        gameOverUI.SetActive(true);

        finalScoreText.text = tileManager.GetScore().ToString();
    }

    public void OnEasyClicked()
    {
        tileManager.SetDifficultyToEasy();
        ShowStartUI();
        difficultyText.text = "Easy";
    }

    public void OnMediumClicked()
    {
        tileManager.SetDifficultyToMedium();
        ShowStartUI();
        difficultyText.text = "Medium";
    }

    public void OnHardClicked()
    {
        tileManager.SetDifficultyToHard();
        ShowStartUI();
        difficultyText.text = "Hard";
    }

}
