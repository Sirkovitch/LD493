using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Flow_Manager : MonoBehaviour
{
    public bool start = false;
    public bool releasePikus = false;
    private Animator effects;
    public GameObject captain;
    public GameObject gameOver;
    public GameObject[] texts;
    public GameObject[] pikuUi;
    private Piku_Manager pikuManager;
    public GameObject startText;

    void Start()
    {
        pikuManager = GameObject.Find("Piku_Manager").GetComponent<Piku_Manager>();
        effects = GameObject.Find("Effects").GetComponent<Animator>();
        start = false;
        releasePikus = false;

        StartCoroutine(Intro(5f));
    }
    IEnumerator Intro(float time)
    {
        yield return new WaitForSeconds(1);
        yield return new WaitWhile(() => !Input.anyKeyDown);
        startText.SetActive(false);

        yield return new WaitForSeconds(3);
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
        foreach ( GameObject pikui in pikuUi)
        {
            pikui.SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        captain.SetActive(false);

    }
    IEnumerator GameOver(float time)
    {
        yield return new WaitForSeconds(4);
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2);
        yield return new WaitWhile(() => !Input.anyKeyDown);
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        if (pikuManager.gameOver == true)
        {
            StartCoroutine(GameOver(5f));
        }
    }
}
