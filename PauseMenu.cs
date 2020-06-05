using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PauseMenu : MonoBehaviour
{


    public static bool isPaused = false;

    public GameObject pauseMenuHolder;
    public bool exitOpen = false;
    public Canvas exitInfo;

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
            
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }

    public void PauseGame ()
    {
        pauseMenuHolder.SetActive(true);
        //speed 0 yap
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuHolder.SetActive(false);
        //speed eski haline çevir
        isPaused = false;
    }

    public void LoadMenu()
    {
        Application.LoadLevel("menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitCanvas()
    {
        if (exitOpen == false)
        {
            exitOpen = true;
            exitInfo.enabled = true;
            pauseMenuHolder.SetActive(false);
        }
        else
        {
            exitOpen = false;
            exitInfo.enabled = false;
            pauseMenuHolder.SetActive(true);
        }
    }

}
