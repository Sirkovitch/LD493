using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PikuCounter : MonoBehaviour
{
    private Transform pikuZone;
    void Start()
    {
        pikuZone = GameObject.Find("PikuSpace").GetComponent<Transform>();
    }

    void Update()
    {
        this.GetComponent<Text>().text = "x " + pikuZone.childCount;
    }
}
