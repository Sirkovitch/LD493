using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eolienne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetFloat("Offset", Random.value);
    }


}
