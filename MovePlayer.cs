using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class MovePlayer : MonoBehaviour {

    public KeyCode moveL;
    public KeyCode moveR;

    public float HorizVel = 0;
    public int laneNum = 0;


    //public bool laneChangeCont = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update () {

        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3);


        //StartCoroutine(stopLaneMove());

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Lethal")
        {
            Destroy(gameObject);
        }
    }

    /*IEnumerator stopLaneMove()
    {

        yield return new WaitForSeconds(.5f);
        HorizVel = 0;
        laneChangeCont = true;

    }*/
}
