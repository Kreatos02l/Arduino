﻿using UnityEngine;
using System.Collections;
using System;
using System.IO.Ports;


public class MoveCam : MonoBehaviour
{

    SerialPort stream;

    public GameObject target; // is the gameobject to

    //	float acc_normalizer_factor = 0.00025f;
    float gyro_normalizer_factor = 1.0f / 32768.0f;   // 32768 is max value captured during test on imu

    //float curr_angle_x = 0;
    float curr_angle_y = 0;
    float curr_angle_z = 0;
    //	float kayip_deger_y = 0.1f;
    //	float kayip_deger_z = 0.1f;
    int ct = 0;
    float curr_offset_x = 0;
    float curr_offset_y = 0;
    float curr_offset_z = 0;

    // Increase the speed/influence rotation
    public float factor = 7;
    public float rotDeg;

    public bool enableRotation;
    public bool enableTranslation;

    // SELECT YOUR COM PORT AND BAUDRATE
    string port = "COM4";
    int baudrate = 9600;
    int readTimeout = 20;

    void Start()
    {
        // open port. Be shure in unity edit > project settings > player is NET2.0 and not NET2.0Subset
        stream = new SerialPort("\\\\.\\" + port, baudrate);

        try
        {
            stream.ReadTimeout = readTimeout;
        }
        catch (System.IO.IOException ioe)
        {
            Debug.Log("IOException: " + ioe.Message);
        }

        stream.Open();
    }

    void Update()
    {
        string dataString = "null received";

        if (stream.IsOpen)
        {
            try
            {
                dataString = stream.ReadLine();
                //Debug.Log("RCV_ : " + dataString);
            }
            catch (System.IO.IOException ioe)
            {
                Debug.Log("IOException: " + ioe.Message);
            }

        }
        else
            dataString = "NOT OPEN";
        //Debug.Log ("RCV_ : " + dataString);

        if (!dataString.Equals("NOT OPEN"))
        {
            // recived string is  like  "accx;accy;accz;gyrox;gyroy;gyroz"
            char splitChar = ';';
            string[] dataRaw = dataString.Split(splitChar);

            // normalized accelerometer values
            //		float ax = Int32.Parse(dataRaw[0]) * acc_normalizer_factor;
            //		float ay = Int32.Parse(dataRaw[1]) * acc_normalizer_factor;
            //		float az = Int32.Parse(dataRaw[2]) * acc_normalizer_factor;

            // normalized gyrocope values
            float gx = Int32.Parse(dataRaw[0]) * gyro_normalizer_factor;
            float gy = Int32.Parse(dataRaw[1]) * gyro_normalizer_factor;
            float gz = Int32.Parse(dataRaw[2]) * gyro_normalizer_factor;

            // 		prevent
            //		if (Mathf.Abs(ax) - 1 < 3) ax = 0;
            //		if (Mathf.Abs(ay) - 1 < 20) ay = 0;
            //		if (Mathf.Abs(az) - 1 < 3) az = 0;


            //		curr_offset_x += ax;
            //		curr_offset_y += ay;
            //		curr_offset_z += az; // The IMU module have value of z axis of 16600 caused by gravity
            // prevent little noise effect
            //	if (Mathf.Abs(gx) < 0.025f) gx = 0f;
            if (Mathf.Abs(gy) < 0.1f) gy = 0f;
            if (Mathf.Abs(gz) < 0.1f) gz = 0f;
            //		if (Mathf.Abs(gy) - 1 < 0) ay = 0;
            //		if (Mathf.Abs(gz) - 1 < 0) az = 0;
            //		curr_angle_x += gx;
            //		if ( gy <= curr_angle_y - 0.19 || gy >= curr_angle_y + 0.19 )
            //		if (gy < 0)
            //			gy = gy - kayip_deger_y;
            //		else if (gy > 0)
            //			gy = gy + kayip_deger_y;
            curr_angle_y += gy;
            //		if (gz <= curr_angle_z - 0.19 || gz >= curr_angle_z + 0.19 ) + curr_angle_y + "
            curr_angle_z += gz;
            Debug.Log("RCV_edddddd:"  + curr_angle_z);
            //if (enableTranslation) target.transform.position = new Vector3(curr_offset_x, curr_offset_z, curr_offset_y);
            //if (enableRotation) target.transform.rotation = Quaternion.Euler(0 * factor, -curr_angle_z * factor, curr_angle_y * factor);
            if (curr_angle_z > 0.1)
            {
                for (rotDeg = 0; rotDeg <= 45; rotDeg += 0.0000005f)
                    target.transform.Rotate(0, 0 ,rotDeg);
            }
        }
    }

}