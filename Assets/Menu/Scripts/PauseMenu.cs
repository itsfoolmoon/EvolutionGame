using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PauseManager pauseManager;
    
    public void ResumeGame()
    {
        pauseManager.ResumeGame();
    }

    public void ExitToMenu()
    {
        pauseManager.ResumeGame(); // Resume game before exiting
        SceneManager.LoadScene(0); // Load the first scene (expected to be Main Menu)
    }
}
