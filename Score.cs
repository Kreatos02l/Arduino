using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour {

    public static float score = 0.0f;
    public float a;
    public Text scoreText;
    public Text timeText;
    public DeathMenu deathMenu;
    public bool isDead = false;

    void Start () {
		a = Menu.cc;
	}
	
	
	void Update () {
        if (isDead)
            return;
        
        Menu.cc -= Time.deltaTime;
        scoreText.text = ((int)score).ToString();
        timeText.text = ((int)Menu.cc).ToString();
        if (Menu.cc<0.0f)
        {
            OnDeath();
        }
	}

    public void OnDeath()
    {

            isDead = true;
            if (PlayerPrefs.GetFloat("Highscore") < score)
                PlayerPrefs.SetFloat("Highscore", score);

            deathMenu.ToogleEndMenu(score,a);
            score = 0;
    
    }
    
}
