using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
    public Text timeText;

    public string id;
    public static string a;

    void Start () {
        gameObject.SetActive(false);
	}

	void Update () {
		
	}



    public void toMenu()
    {
        SceneManager.LoadScene("menu");
    }
    public void ToogleEndMenu(float score, float a)
    {
        id = addPatient.id;
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        timeText.text = ((int)a).ToString();
        StartCoroutine(Register(score,id));
    }

    IEnumerator Register(float score, string id)
    {
        a = Convert.ToString(score);
        UnityWebRequest link = UnityWebRequest.Get("http://localhost/php/updatescore.php?score=" + a + "&id=" + id);
        yield return link.SendWebRequest();
       
      
    }

}
