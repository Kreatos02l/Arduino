using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class adminLogin : MonoBehaviour
{

    public string username;
    public Text info;
    public InputField nameField;
    public InputField passwordField;
    public Canvas loginCanvas;

    private void Start()
    {
        loginCanvas.enabled = false;
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/php/adminlogin.php", form);

        yield return www;

        if (www.text[0] == '0')
        {

            loginCanvas.enabled = true;

        }
        else
            info.text = www.text;

    }

}