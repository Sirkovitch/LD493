using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Manager : MonoBehaviour
{
    public bool start = false;
    public bool releasePikus = false;
    private Animator effects;
    public GameObject captain;
    public GameObject[] texts;

    void Start()
    {
        effects = GameObject.Find("Effects").GetComponent<Animator>();
        start = false;
        releasePikus = false;

        StartCoroutine(Intro(5f));
    }
    IEnumerator Intro(float time)
    {
        yield return new WaitForSeconds(1);
        yield return new WaitWhile(() => !Input.anyKeyDown);

        yield return new WaitForSeconds(2);
        captain.SetActive(true);

        yield return new WaitForSeconds(1);
        int numTexts = texts.Length;
        for(int i = 0; i< numTexts; i++)
        {
            captain.GetComponent<Animator>().SetTrigger("Talk");
            texts[i].SetActive(true);
            yield return new WaitWhile (() => !Input.anyKeyDown);
            texts[i].SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        captain.GetComponent<Animator>().SetTrigger("Exit");

        yield return new WaitForSeconds(0.2f);

        effects.SetBool("Start", true);
        start = true;
        releasePikus = true;

        yield return new WaitForSeconds(1f);
        captain.SetActive(false);

    }

    void Update()
    {

    }
}
