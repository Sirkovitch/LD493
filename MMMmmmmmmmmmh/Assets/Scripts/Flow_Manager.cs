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
    public GameObject win;
    public GameObject[] texts;
    public GameObject[] pikuUi;
    private Piku_Manager pikuManager;
    private Plane_Manager planeManager;
    public GameObject startText;
    public AudioSource musicManager;
    public AudioClip intro01, intro02, intro03, music;

    void Start()
    {
        pikuManager = GameObject.Find("Piku_Manager").GetComponent<Piku_Manager>();
        planeManager = GameObject.Find("Plane").GetComponent<Plane_Manager>();
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

        musicManager.clip = intro01;
        musicManager.Play();
        StartCoroutine(AudioSwitch1());

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

        StartCoroutine(AudioSwitch2());

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
    IEnumerator AudioSwitch1()
    {
        yield return new WaitForSeconds(musicManager.clip.length);
        musicManager.clip = intro02;
        musicManager.Play();

    }
    IEnumerator AudioSwitch2()
    {
        musicManager.clip = intro03;
        musicManager.Play();
        yield return new WaitForSeconds(musicManager.clip.length);
        musicManager.clip = music;
        musicManager.Play();

    }

    IEnumerator GameOver(float time)
    {
        yield return new WaitForSeconds(3);
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2);
        yield return new WaitWhile(() => !Input.anyKeyDown);
        SceneManager.LoadScene("Game");
    }
    IEnumerator Win(float time)
    {
        yield return new WaitForSeconds(3);
        win.SetActive(true);
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
        if (planeManager.landing == true)
        {
            StartCoroutine(Win(5f));
        }
    }
}
