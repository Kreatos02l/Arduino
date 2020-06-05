using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    // Use this for initialization
    public GameObject coin;
    float randX;
    float randZ;
    Vector3 whereToSpawn;
    public float spawnRate = 0.001f;
    public float nextSpawn = 0.0f;
    

    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
     
        if (Time.time > spawnRate)
        {            

            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-1f, 1f);
            randZ = Random.Range(2f, 10000f);
            whereToSpawn = new Vector3(randX, transform.position.y, randZ);
            Instantiate(coin, whereToSpawn, Quaternion.Euler(90,0,0));
        }
	}
}
