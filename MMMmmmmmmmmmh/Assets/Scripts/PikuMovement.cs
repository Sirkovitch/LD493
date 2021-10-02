using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikuMovement : MonoBehaviour
{
    public float speed = 0.02f;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetButton("Left"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x -  speed, transform.localPosition.y, transform.localPosition.z);
        }
        if (Input.GetButton("Right"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x + speed, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
