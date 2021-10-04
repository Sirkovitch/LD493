using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundManager : MonoBehaviour
{
    public bool playProblem = false;
    public bool playSmoke = false;
    public bool stop = true;

    private AudioSource engineSound;
    public AudioClip engineProblem, engineExplose, engineSmoke;

    private float myPitch = 0;
    void Start()
    {
        engineSound = this.GetComponent<AudioSource>();
        myPitch = 0;
    }

    void Update()
    {
        if (playProblem == true)
        {
            if (engineSound.clip != engineProblem)
            {
                engineSound.clip = engineProblem;
                engineSound.Play();
                engineSound.volume = 1.5f;
            }
            engineSound.pitch = Mathf.Lerp(myPitch,2,0.01f);
            myPitch = engineSound.pitch;
        }
        if (playSmoke == true)
        {
            if (engineSound.clip != engineSmoke)
            {
                engineSound.clip = engineSmoke;
                engineSound.Play();
                engineSound.volume = 1.5f;
                engineSound.pitch = 1;

                myPitch = 0;
            }
        }
        if (stop == true)
        {
            engineSound.Stop();
        }
    }
}
