using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Manager : MonoBehaviour
{
    public Collider wingArea;
    public Transform pikuParent;
    public Animator engineR, engineL;

    private Vector3 spawnZoneSize;
    private float playArea;
    private float globalPos = 0;
    public float tiltValue;
    private Vector3 velocity, myVelocity;
    private Piku_Manager pikuManager;
    private Flow_Manager flowManager;

    private bool collided = false;
    private bool nearEnd = false;
    public bool landing = false;
    private float initDelay = 3;
    public AudioClip explode;

    private bool engineRBroken, engineLBroken, problem;

    void Start()
    {
        spawnZoneSize = wingArea.bounds.size;
        playArea = spawnZoneSize.x / 2;
        tiltValue = 0;
        pikuManager = GameObject.Find("Piku_Manager").GetComponent<Piku_Manager>();
        engineRBroken = false;
        engineLBroken = false;
        problem = false;
        flowManager = GameObject.Find("FlowManager").GetComponent<Flow_Manager>();
        myVelocity = new Vector3(0, 0, 20);
    }

    void Update()
    {
        if (pikuParent.childCount == 0 && pikuManager.allPiku == true)
        {
            this.GetComponent<Collider>().isTrigger = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }

        Transform[] pikus = pikuParent.transform.GetComponentsInChildren<Transform>();

        if (pikus.Length > 1 && pikuManager.allPiku == true)
        {
            foreach (Transform piku in pikus)
            {
                globalPos = globalPos + piku.transform.localPosition.x;
            }
            globalPos = globalPos / (pikus.Length - 1);
            tiltValue = globalPos / playArea;
            tiltValue = -tiltValue*3;
            globalPos = 0;
        }
        else
        {
            tiltValue = 0;
        }

        if (pikus.Length > 1)
        {
            tiltValue = Mathf.Lerp(tiltValue, tiltValue * 0.1f, Mathf.Clamp((Mathf.Abs(this.transform.localRotation.z)-0.3f) * 2, 0, 1));
        }

        if (engineRBroken == true)
        {
            tiltValue = tiltValue - 0.175f;
        }
        if (engineLBroken == true)
        {
            tiltValue = tiltValue + 0.175f;
        }
        if (collided == true || landing == true)
        {
            tiltValue = 0;
            //this.transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, 0.01f);
        }



        this.transform.Rotate(0, 0, tiltValue, Space.Self);

        if (flowManager.start == true && collided == false && landing == false)
        {
            velocity = new Vector3(Mathf.Clamp(1 - this.transform.localRotation.z * 100, -50, 10), -1, 20);
        }
        else if (landing == true)
        {
            velocity = Vector3.Lerp(myVelocity, new Vector3(0,0,0),0.005f);
            myVelocity = velocity;
        }
           else
        {
            velocity = new Vector3(0, 0, 0);
        }

        transform.Translate(velocity * Time.deltaTime, Space.World);

        //Broken Engines
        if (problem == false && flowManager.start == true && nearEnd == false)
        {
            StartCoroutine(EngineProblem(Random.value*5+5));
        }

    }
    IEnumerator EngineProblem(float time)
    {
        problem = true;
        yield return new WaitForSeconds(initDelay);
        initDelay = 0;
        yield return new WaitForSeconds(time);

        if (Random.value > 0.5)
        {
            engineR.SetBool("Broken", true);

            yield return new WaitForSeconds(4);

            engineRBroken = true;
            engineR.SetBool("Boum", true);

            yield return new WaitForSeconds(10);

            engineR.SetBool("Broken", false);
            engineR.SetBool("Boum", false);
            engineRBroken = false;
            problem = false;
        }
        else
        {
            engineL.SetBool("Broken", true);

            yield return new WaitForSeconds(3);

            engineLBroken = true;
            engineL.SetBool("Boum", true);

            yield return new WaitForSeconds(10);

            engineL.SetBool("Broken", false);
            engineL.SetBool("Boum", false);
            engineLBroken = false;
            problem = false;
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Obstacle" && collided == false)
        {
            this.GetComponent<Collider>().isTrigger = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            collided = true;
            pikuParent.DetachChildren();
            this.GetComponent<AudioSource>().clip = explode;
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<AudioSource>().loop = false;
        }
        if (col.tag == "Arrival")
        {
            landing = true;
        }
        if (col.tag == "nearEnd")
        {
            nearEnd = true;
        }
    }
}
