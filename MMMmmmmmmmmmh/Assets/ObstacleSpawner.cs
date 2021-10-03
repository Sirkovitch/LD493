using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private Vector3 spawnZoneSize, spawnZoneCenter, spawnPos;
    public int obstacleNum = 20;
    public GameObject[] obstacles;
    void Start()
    {
        spawnZoneSize = this.GetComponent<Collider>().bounds.size;
        spawnZoneCenter = this.GetComponent<Collider>().bounds.center;
        for (int i = 0; i < obstacleNum; i++)
        {
            float spawnPosX = Random.value * spawnZoneSize.x - (spawnZoneSize.x / 2);
            float spawnPosY = Random.value * spawnZoneSize.y - (spawnZoneSize.y / 2);
            float spawnPosZ = Random.value * spawnZoneSize.z - (spawnZoneSize.z / 2);

            spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
            
            if (Random.value > 0.5f)
            {
                GameObject.Instantiate(obstacles[1], spawnPos + spawnZoneCenter, Quaternion.identity);
            }
            else
            {
                GameObject.Instantiate(obstacles[0], spawnPos + spawnZoneCenter, Quaternion.identity);
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
