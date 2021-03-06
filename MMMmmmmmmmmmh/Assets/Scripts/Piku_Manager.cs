using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piku_Manager : MonoBehaviour
{
    public bool gameOver = false;
    public int pikuNum;
    public GameObject pikuPrefab;
    public GameObject spawnZone;
    public Transform pikuZone;
    public float initForceValueX = 3;
    public float initForceValueY = 10;
    public Animator trap;

    public bool allPiku = false;
    private Vector3 spawnZoneSize, spawnPos, spawnZoneCenter;
    private int spawnedPiku = 0;
    private Flow_Manager flowManager;

    


    void Start()
    {
        gameOver = false;
        
        spawnZoneSize = spawnZone.GetComponent<Collider>().bounds.size;
        spawnZoneCenter = spawnZone.GetComponent<Collider>().bounds.center;

        flowManager = GameObject.Find("FlowManager").GetComponent<Flow_Manager>();


    }
    
    void SpawnPiku()
    {
        
        float spawnPosX = Random.value * spawnZoneSize.x - (spawnZoneSize.x / 2);
        float spawnPosY = Random.value * spawnZoneSize.y - (spawnZoneSize.y / 2);
        float spawnPosZ = Random.value * spawnZoneSize.z - (spawnZoneSize.z / 2);

        spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        spawnZoneCenter = spawnZone.GetComponent<Collider>().bounds.center;
        var newPiku = Instantiate(pikuPrefab, spawnPos + spawnZoneCenter, Quaternion.identity);
        newPiku.transform.parent = pikuZone;
        var rB = newPiku.GetComponent<Rigidbody>();
        rB.velocity = new Vector3((Random.value) * initForceValueX - (initForceValueX / 2), initForceValueY, 0);
        spawnedPiku = spawnedPiku+1;
    }
 
    void Update()
    {
        if (flowManager.releasePikus == true)
        {
            InvokeRepeating("SpawnPiku", 1f, 0.1f);
            trap.SetBool("Open", true);
            flowManager.releasePikus = false;
        }

        if (spawnedPiku >= pikuNum)
        {
            CancelInvoke();
            trap.SetBool("Open", false);
            StartCoroutine(startDelay(2f));

        }
        if (allPiku == true && pikuZone.childCount == 0)
        {
            gameOver = true;
        }
    }
    IEnumerator startDelay(float time)
    {
        yield return new WaitForSeconds(time);
        allPiku = true;
    }
}
