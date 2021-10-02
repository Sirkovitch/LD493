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
    public float tiltValue;
    private float rotationDegree = 0;

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
        
        var lerpValue = 0.02f;
        lerpValue = Mathf.Lerp(0.0000001f, 0.5f, (pikus.Length - 1) / 25);
        var rotateValue = tiltValue;
         rotateValue = Mathf.Lerp(tiltValue*0.5f,tiltValue, (pikus.Length - 1) / 25);

        rotationDegree = Mathf.Lerp(rotationDegree, rotateValue, lerpValue);
        this.transform.Rotate(0, 0, tiltValue, Space.Self);
    }
}
