using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piku_Manager : MonoBehaviour
{
    public int pikuNum;
    public GameObject pikuPrefab;
    public GameObject spawnZone;
    public Transform pikuZone;
    public float initForceValueX = 3;
    public float initForceValueY = 10;
    public Animator trap;

    private Vector3 spawnZoneSize, spawnPos, spawnZoneCenter;
    private int spawnedPiku = 0;


    void Start()
    {
        spawnZoneSize = spawnZone.GetComponent<Collider>().bounds.size;
        spawnZoneCenter = spawnZone.GetComponent<Collider>().bounds.center;

        InvokeRepeating("SpawnPiku", 1f, 0.1f);
        trap.SetBool("Open", true);


    }
    
    void SpawnPiku()
    {
        
        float spawnPosX = Random.value * spawnZoneSize.x - (spawnZoneSize.x / 2);
        float spawnPosY = Random.value * spawnZoneSize.y - (spawnZoneSize.y / 2);
        float spawnPosZ = Random.value * spawnZoneSize.z - (spawnZoneSize.z / 2);

        spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        var newPiku = Instantiate(pikuPrefab, spawnPos + spawnZoneCenter, Quaternion.identity);
        newPiku.transform.parent = pikuZone;
        var rB = newPiku.GetComponent<Rigidbody>();
        rB.velocity = new Vector3((Random.value) * initForceValueX - (initForceValueX / 2), initForceValueY, 0);
        spawnedPiku = spawnedPiku+1;
    }
 
    void Update()
    {
        if (spawnedPiku >= pikuNum)
        {
            CancelInvoke();
            trap.SetBool("Open", false);
        }
    }
}
