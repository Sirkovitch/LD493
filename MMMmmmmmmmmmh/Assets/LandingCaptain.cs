using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingCaptain : MonoBehaviour
{
    public GameObject dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            dialogue.SetActive(true);
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
