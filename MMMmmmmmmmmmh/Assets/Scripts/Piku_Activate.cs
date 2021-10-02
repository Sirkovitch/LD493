using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piku_Activate : MonoBehaviour
{
    public float activationTime = 1;

    bool canCollide = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(activationTime));
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider wing)
    {
        if (canCollide == true && wing.tag == "Wing")
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
}
