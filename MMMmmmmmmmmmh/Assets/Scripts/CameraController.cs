using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform plane;
    public Vector3 initTransform = new Vector3(0f, 9.7f, -12.7f);
    private Vector3 planePos;
    private Vector3 planeDist;
    public Transform menuCam;
    private Quaternion initRot, menuRot;

    private bool playCam = false;
    private bool moveCam = false;

    void Start()
    {
        playCam = false;
        moveCam = false;
        plane = GameObject.Find("Plane").GetComponent<Transform>();
        planePos = plane.position;
        initRot = this.transform.rotation;
        planeDist = planePos - initTransform;
        transform.position = menuCam.position;
        transform.rotation = menuCam.rotation;
        StartCoroutine(Menu(5f));

    }

    IEnumerator Menu(float time)
    {
        yield return new WaitForSeconds(1);
        yield return new WaitWhile(() => !Input.anyKeyDown);
        moveCam = true;
    }

    void LateUpdate()
    {
        if (moveCam == true)
        {
            transform.position = Vector3.Lerp(transform.position, initTransform, 0.025f);
            transform.rotation = Quaternion.Lerp(transform.rotation, initRot, 0.025f);
        }
        if (transform.position.x-initTransform.x < 0.01)
        {
            moveCam = false;
            playCam = true;
        }
        if (playCam == true)
        {
            transform.position = plane.position - planeDist;
        }
    }
}
