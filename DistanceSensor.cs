using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class DistanceSensor : MonoBehaviour
{

    public float distance;
    SerialPort stream = new SerialPort("COM4", 9600);
    void Start()
    {
        stream.Open();
        stream.ReadTimeout = 25;
    }


    void Update()
    {
        Vector3 temp = transform.position;
        if (stream.IsOpen)
        {
            try
            {
                float data = stream.ReadByte();
                data = Mathf.Clamp(data, 2, 3);
                data -= 1;
                
                data -= 2;
                temp.x = data;
                Debug.Log(data);
            }

            catch (System.Exception)
            {
                Debug.Log("timeout");
                stream.Open();
            }

            transform.position = temp;



        }


    }
}




