using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piku_Activate : MonoBehaviour
{
    public float activationTime = 0.2f;
    public float moveTime = 4;
    public float offsetMult = 2;
    public Material[] materials;
    public Renderer rend;
    public Animator anim;
    public AudioClip[] screams;
    public AudioClip[] steps;

    bool canCollide = false;
    bool canMove = false;
    private Transform destination;
    private float randPos;
    private float randSpeed;

    private bool stepping = false;

    private bool screamed = false;

    // Start is called before the first frame update
    void Start()
    {
        var randScale = Random.value * 0.4f;
        this.transform.localScale = new Vector3(this.transform.localScale.x + randScale, this.transform.localScale.y + randScale, this.transform.localScale.z + randScale);

        rend.material = materials[Random.Range(0, materials.Length)];
        
        destination = GameObject.Find("PikuDestination").GetComponent<Transform>();
        
        randPos = (Random.value * 2) - 1;
        randPos = randPos * offsetMult;

        randSpeed = Random.value * 0.2f;

        StartCoroutine(ActivateCol(activationTime));
        StartCoroutine(ActivateMov(moveTime));
    }


    private void OnTriggerEnter(Collider col)
    {
        if (canCollide == true && col.tag == "Wing")
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            canCollide = false;
        }
        if (col.tag == "EndZone")
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetComponent<BoxCollider>().isTrigger = false;
            canMove = false;
            transform.parent = null;
        }
    }

    private void Update()
    {
        if (canMove == true)
        {
            var direction = (destination.localPosition.x + randPos) - transform.localPosition.x;
            anim.SetFloat("Blend", direction / 1);

            if (transform.parent != null && Mathf.Abs(direction) > 0.5 && stepping == false)
            {
                if (Random.value>0.95f)
                {
                    this.GetComponent<AudioSource>().loop = true;
                    this.GetComponent<AudioSource>().clip = steps[Random.Range(0, steps.Length)];
                    this.GetComponent<AudioSource>().Play();
                }
                stepping = true;
            }
            else
            {
                this.GetComponent<AudioSource>().loop = false;
                stepping = false;
            }
            if (transform.parent == null)
            {
                this.GetComponent<AudioSource>().loop = false;
                stepping = false;
            }

                transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x,destination.localPosition.x+randPos,randSpeed), transform.localPosition.y, transform.localPosition.z);

        }
        if (Mathf.Abs(transform.rotation.z) > Mathf.Lerp(0.7f, 0.55f, Mathf.Clamp01(Mathf.Abs(transform.localPosition.x) / 10)))
        {
            transform.parent = null;
        }
        if (transform.parent == null && screamed == false)
        {
            this.GetComponent<AudioSource>().clip = screams[Random.Range(0, screams.Length)];
            this.GetComponent<AudioSource>().volume = 1;
            this.GetComponent<AudioSource>().loop = false;
            this.GetComponent<AudioSource>().Play();
            screamed = true;
        }

    }

    IEnumerator ActivateCol(float time)
    {
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
    IEnumerator ActivateMov(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

}
