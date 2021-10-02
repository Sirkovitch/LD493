using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Manager : MonoBehaviour
{
    public Collider wingArea;
    public Transform pikuParent;

    private Vector3 spawnZoneSize;
    private float playArea;
    private float globalPos;
    private float tiltValue;

    void Start()
    {
        spawnZoneSize = wingArea.bounds.size;
        playArea = spawnZoneSize.x / 2;
        tiltValue = 0;
    }

    void Update()
    {
        Transform[] pikus = pikuParent.transform.GetComponentsInChildren<Transform>();
        if (pikus.Length > 1)
        {
            foreach (Transform piku in pikus)
            {
                globalPos = globalPos + piku.transform.localPosition.x;
            }
            globalPos = globalPos / (pikus.Length - 1);
            tiltValue = globalPos / playArea;
            tiltValue = -tiltValue;
            globalPos = 0;
        }
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, Mathf.Lerp(this.transform.eulerAngles.z, tiltValue * 80, 0.01f));
    }
}
