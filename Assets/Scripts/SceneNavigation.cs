using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadInstructionMenu()
    {
        SceneManager.LoadScene("InstructionsScene");
    }
}

