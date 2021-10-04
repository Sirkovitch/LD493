using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomParaglider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetFloat("Offset", Random.value);
        if (Random.value > 0.5f)
        {
            this.GetComponent<Animator>().SetBool("Mirror", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
