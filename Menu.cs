using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour {

	public Canvas exitInfo;
    public Canvas settingsInfo;
	public bool exitOpen=false;
    public bool settingsOpen = false;
    public Text highscoreText;
    public Canvas controlsInfo;
    public Canvas statisticInfo;
    public bool statisticOpen = false;
    public bool controlsOpen = false;
    public Canvas Maps;
    public InputField time;
    public static float cc;


    void Start()
    {
        highscoreText.text = "Highscore : " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();
        controlsInfo.enabled = false;
        Maps.enabled = false;
        statisticInfo.enabled = false;
    }
    public void StartGame ()
	{
        Maps.enabled = true;
	}

    public void Ground()
    {
        cc = float.Parse(time.text);
        Application.LoadLevel("Level1-ground");       
    }
    public void Leaf()
    {
        cc = float.Parse(time.text);
        Application.LoadLevel("Level2-leaf");
    }
    public void Snow()
    {
        cc = float.Parse(time.text);
        Application.LoadLevel("Level3-snow");
    }
    public void ExitGame()
	{
		Application.Quit();
	}

    public void ChangePatient()
    {
        Application.LoadLevel("Login");
    }

	public void ExitCanvas()
	{
		if (exitOpen == false) {
			exitOpen = true;
			exitInfo.enabled = true;
            settingsOpen = false;
            settingsInfo.enabled = false;
        }
		else 
		{
			exitOpen = false;
			exitInfo.enabled = false;
		}
	}

    public void SettingsCanvas()
    {
        if (settingsOpen == false)
        {
            settingsOpen = true;
            settingsInfo.enabled = true;
            exitOpen = false;
            exitInfo.enabled = false;
        }
        else
        {
            settingsOpen = false;
            settingsInfo.enabled = false;
        }
    }

    public void ControlsCanvas()
    {
        if (controlsOpen == true)
        {
            controlsOpen = false;
            controlsInfo.enabled = false;
        }
        else
        {
            controlsOpen = true;
            controlsInfo.enabled = true;
        }
    }

    public void StatisticCanvas()
    {
        if (statisticOpen == true)
        {
            statisticOpen = false;
            statisticInfo.enabled = false;
        }
        else
        {
            statisticOpen = true;
            statisticInfo.enabled = true;
        }
    }


}
