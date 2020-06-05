using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DisplayPatient : MonoBehaviour
{


    public string id;
    public string[] items = new string[10];
    public Text[] texts;
    public void CallDisplay()
    {
        id = addPatient.id;

        StartCoroutine(Display(id));
    }

    IEnumerator Display(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("komut", "veriCekme");
        WWW w = new WWW("http://localhost/php/displaypatient.php?id=" +id, form);
        yield return w;
        items = w.text.Split(';');
        for(int i=0; i<items.Length;i++)
        {
            texts[i].text = "" + items[i];
        }
        Debug.Log(w.text);
    }



}