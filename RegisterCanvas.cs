using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterCanvas : MonoBehaviour {

    public Canvas main;
    public Canvas register;
	
    public void MainRegister()
    {
        main.enabled = false;
        register.enabled = true;
    }
}
