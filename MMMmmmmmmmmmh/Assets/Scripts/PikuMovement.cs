using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikuMovement : MonoBehaviour
{
    public float speed = 0.02f;

    private Plane_Manager planeManager;
    private Transform plane;
    private float moveSpeed;

    void Start()
    {
        planeManager = GameObject.Find("Plane").GetComponent<Plane_Manager>();
        plane = GameObject.Find("Plane").GetComponent<Transform>();
    }


    void Update()
    {

        var planeAngle = plane.transform.localRotation.z*2;

        if (planeAngle >= 0)
        {
            moveSpeed = speed * (1 - planeAngle);
        }
        else
        {
            moveSpeed = speed * (1 - Mathf.Abs(planeAngle));

        }


        transform.localPosition = new Vector3(transform.localPosition.x - planeAngle*0.01f, transform.localPosition.y, transform.localPosition.z);

        if (Input.GetButton("Left"))
        {

            transform.localPosition = new Vector3(transform.localPosition.x - moveSpeed, transform.localPosition.y, transform.localPosition.z);
        }
        if (Input.GetButton("Right"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x + moveSpeed, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
