using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform plane;
    private Vector3 initTransform = new Vector3(0f, 9.7f, -12.7f);
    private Vector3 planePos;
    private Vector3 planeDist;

    void Start()
    {
        plane = GameObject.Find("Plane").GetComponent<Transform>();
        planePos = plane.position;
        planeDist = planePos - initTransform;

    }

    void LateUpdate()
    {
        transform.position = plane.position - planeDist;
    }
}
