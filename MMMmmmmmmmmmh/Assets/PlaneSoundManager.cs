using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSoundManager : MonoBehaviour
{
    private AudioSource planeSound;
    void Start()
    {
        planeSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        planeSound.pitch = Mathf.Clamp(1+ Mathf.Abs(this.transform.rotation.z)*2,1,2);
        planeSound.volume = Mathf.Clamp(0.6f + Mathf.Abs(this.transform.rotation.z) * 0.5f,0,0.9f);


    }
}
