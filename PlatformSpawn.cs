using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour {

    public GameObject[] platformPrefabs;
    private Transform playerTransform;
    private float spawnZ = -6.0f;
    private float platformLength = 12.0f;
    private int heightPlatform = 3;
    private float safeZone = 30.0f;
    private int lastPrefabIndex = 0;

    private List<GameObject> activePlatforms;

	private void Start () {
        activePlatforms = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < heightPlatform; i++)
        {
            if (i < 2)
                SpawnPlatform(0);
            else
                SpawnPlatform();
        }
	}
	
	
	private void Update () {
        if (playerTransform.position.z - safeZone > (spawnZ - heightPlatform * platformLength))
        {
            SpawnPlatform();
            DeletePlatform();
        }
	}

    private void SpawnPlatform(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(platformPrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(platformPrefabs[prefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += platformLength;
        activePlatforms.Add(go);
    }

    private void DeletePlatform()
    {
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if(platformPrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, platformPrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
