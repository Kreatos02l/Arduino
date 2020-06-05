using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class adminRegister : MonoBehaviour {

    public Text error;
    public InputField namefield;
    public InputField passwordfield;
    public InputField passwordfiled2;
    public Canvas register;
    public Canvas main;

    private string name;
    private string password;
    private string password2;
    private string gstring;

    private void Start()
    {
        register.enabled = false;
    }

    public void Submit()
    {
        error.text = "";
        name = namefield.text;
        password = passwordfield.text;
        password2 = passwordfiled2.text;

        if (password == password2)
        {
            StartCoroutine(Register(name, password));
        }
        else
            error.text = "Passwords doesnt match";
    }
    IEnumerator Register(string name,string password)
    {
        UnityWebRequest link = UnityWebRequest.Get("http://localhost/php/adminregister.php?username=" + name + "&password=" + password);
        yield return link.SendWebRequest();

        gstring = link.downloadHandler.text;

        show_error(gstring.ToString());
    }
    private void show_error(string k)
    {
        string[] arr = null;
        arr = k.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
        error.text = k;
        if(k!= "Username already exist")
        {
            register.enabled = false;
            main.enabled = true;
        }
    }



}
