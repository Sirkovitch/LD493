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
    private Vector3 velocity;
    private Piku_Manager pikuManager;

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
    }

    void Update()
    {
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



        this.transform.Rotate(0, 0, tiltValue, Space.Self);

        velocity = new Vector3(Mathf.Clamp(1-this.transform.localRotation.z*100,-50,10), -1, 20);
        transform.Translate(velocity * Time.deltaTime, Space.World);

        //Broken Engines
        if (problem == false)
        {
            StartCoroutine(EngineProblem(Random.value*5+5));
        }

    }
    IEnumerator EngineProblem(float time)
    {
        problem = true;

        yield return new WaitForSeconds(time);

        if (Random.value > 0.5)
        {
            engineR.SetBool("Broken", true);

            yield return new WaitForSeconds(3);

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
}
