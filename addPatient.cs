using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class addPatient : MonoBehaviour
{

    public Text error;
    public InputField namefield;
    public InputField surnamefield;
    public InputField idfield;
    public InputField agefield;
    public InputField typefield;
    public Canvas login;


    public static string name;
    public static string surname;
    public static string id;
    public static string age;
    public static string type;

    private string gstring;


    public void Submit()
    {
        error.text = "";
        name = namefield.text;
        surname = surnamefield.text;
        id = idfield.text;
        age = agefield.text;
        type = typefield.text;
        if(name != null && surname != null && id != null && age != null && type != null)
        StartCoroutine(Register(name, surname, id, age, type));
    }
    IEnumerator Register(string name, string surname, string id, string age, string type)
    {
        UnityWebRequest link = UnityWebRequest.Get("http://localhost/php/addpatient.php?name=" + name + "&surname=" + surname + "&id=" + id + "&age=" + age + "&type=" + type);
        yield return link.SendWebRequest();

        gstring = link.downloadHandler.text;

        show_error(gstring.ToString());
    }
    private void show_error(string k)
    {
        string[] arr = null;
        arr = k.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
        error.text = k;
        if (k != "Id already exist")
        {
            login.enabled = false;
            Application.LoadLevel("menu");
        }
    }



}
