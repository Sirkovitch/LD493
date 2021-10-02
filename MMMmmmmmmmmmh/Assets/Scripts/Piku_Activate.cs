using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piku_Activate : MonoBehaviour
{
    public float activationTime = 0.2f;
    public float moveTime = 4;
    public float offsetMult = 2;

    bool canCollide = false;
    bool canMove = false;
    private Transform destination;
    private float randPos;
    private float randSpeed;

    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.Find("PikuDestination").GetComponent<Transform>();
        
        randPos = (Random.value * 2) - 1;
        randPos = randPos * offsetMult;

        randSpeed = Random.value * 0.2f;

        StartCoroutine(ActivateCol(activationTime));
        StartCoroutine(ActivateMov(moveTime));
    }


    private void OnTriggerEnter(Collider col)
    {
        if (canCollide == true && col.tag == "Wing")
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            canCollide = false;
        }
        if (col.tag == "EndZone")
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetComponent<BoxCollider>().isTrigger = false;
            canMove = false;
            transform.parent = null;
        }
    }

    private void Update()
    {
        if (canMove == true)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x,destination.localPosition.x+randPos,randSpeed), transform.localPosition.y, transform.localPosition.z);

        }
    }

    IEnumerator ActivateCol(float time)
    {
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
    IEnumerator ActivateMov(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

}
