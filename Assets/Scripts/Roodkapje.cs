﻿using UnityEngine;

[AddComponentMenu("Roodkapje/Roodkapje")]
public class Roodkapje : MonoBehaviour
{
    public AudioClip[] speech;
    public AudioClip[] gasps;
    public AudioClip[] walks;
    public AudioClip[] whimpers;
    public AudioClip[] shrieks;
    public AudioClip[] gore;

    public GameObject[] gibs;
    public Vector3[] gibPositions;

    public GameObject roodKwakje;
    public GameObject bloem;

    private RoodkapjeManager manager;

    private float fleeRadius;
    private float safeRadius;
    private float deathRadius;

    private float startleTime;

    private Wolf wolf;
    private Vector3 wolfPos;
    private float _distance;

    public float Distance
    {
        get
        {
            return _distance;
        }
    }

    private NavMeshAgent agent;
    private Vector3 destination;

    private AudioSource feet;
    private AudioSource voice;

    /*
     *  State Machine
     */

    public enum RoodkapjeState
    {
        Idle,       //  Nietsvermoedend is Roodkapje bezig bloemen te plukken/een liedje te fluiten/een bolis te boetseren/...
        Startled,   //  De verschijning van de wolf doet Roodkapje opschrikken
        Flee,       //  Roodkapje rent weg van de wolf
        Dead,       //  Roodkapje wordt verslonden door de wolf
        Destroy     //  De dode Roodkapje wordt netjes opgeveegd
    }

    private RoodkapjeState state;
    public RoodkapjeState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
            tState = 0;     //  Reset state timer
        }
    }

    private float tState;   //  Timer variable for state events

    /*
     *  Built-in Functions
     */

	void Start ()
    {
        wolf = FindObjectOfType<Wolf>();

        agent = GetComponent<NavMeshAgent>();

        voice = gameObject.AddComponent<AudioSource>();
        voice.playOnAwake = false;
        voice.pitch = Random.Range(0.85f, 1.25f);
        voice.spatialBlend = 1;

        feet = gameObject.AddComponent<AudioSource>();
        feet.playOnAwake = false;
        feet.pitch = Random.Range(0.85f, 1.15f);
        feet.spatialBlend = 1;

        State = RoodkapjeState.Idle;

        //  Varieer lengte van Roodkapje

        //transform.FindChild("joint7").transform.lossyScale = 3 * Vector3.up;
            //transform.up * Random.Range(0.1f, 1.9f);
	}
	
	void Update ()
    {
        wolfPos = wolf.transform.position;
        _distance = Vector3.Distance(wolfPos, transform.position);

        if (_distance > safeRadius) return;
        if (_distance > deathRadius) State = RoodkapjeState.Destroy;

        switch (State)
        {
            case RoodkapjeState.Idle:

                Ray ray = new Ray(transform.position + Vector3.up * 0.5f, wolfPos - transform.position + Vector3.up * 0.5f);
                RaycastHit hit = new RaycastHit();

                if (_distance <= fleeRadius && Physics.Raycast(ray, out hit) && hit.transform.tag == "Wolf") //  Roodkapje is niet bang voor de grote boze wolf als die niet te zien is
                {
                    State = RoodkapjeState.Startled;
                    
                    voice.PlayOneShot(gasps[Random.Range(0, gasps.Length)], 1.5f);
                }
                else if (tState >= Random.Range(2, 4))
                {
                    agent.SetDestination(transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
                }

                if (tState >= Random.Range(5, 15))
                {
                    voice.PlayOneShot(speech[Random.Range(0, gasps.Length)], 1.5f);
                    tState = 0;
                }

                break;

            case RoodkapjeState.Startled:

                if (tState >= startleTime)
                {
                    State = RoodkapjeState.Flee;

                    voice.clip = whimpers[Random.Range(0, whimpers.Length)];
                    voice.Play();
                }

                break;

            case RoodkapjeState.Flee:

                agent.SetDestination(transform.position + (transform.position - wolfPos) / _distance * safeRadius);

                if (_distance >= safeRadius)
                {
                    agent.SetDestination(transform.position);

                    State = RoodkapjeState.Idle;
                }

                if (!feet.isPlaying)
                {
                    feet.clip = walks[Random.Range(0, walks.Length)];
                    feet.Play();
                }

                if (!voice.isPlaying && tState % 4 < Time.deltaTime)
                {
                    voice.clip = whimpers[Random.Range(0, whimpers.Length)];
                    voice.Play();
                }

                break;

            case RoodkapjeState.Dead:
                if (tState == 0)
                {
                    GetComponentInChildren<Renderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                    //GetComponent<NavMeshAgent>().enabled = false;
                    agent.enabled = false;

                    manager.Kapjes++; //meerkapjes :)

                    manager.BloodLevel += 5;

                    for (int i = 0; i < gibs.Length; i++)
                    {
                        GameObject g = (GameObject)Instantiate(gibs[i], gibPositions[i] + transform.position, transform.rotation);
                    }

                    voice.clip = shrieks[Random.Range(0, shrieks.Length)];
                    voice.Play();

                    feet.clip = gore[Random.Range(0, gore.Length)];
                    feet.Play();

                    Instantiate(roodKwakje, transform.position, Quaternion.identity);   //  Want mathijs wilde meer impact voor een ontploffende roodkap
                    Instantiate(bloem, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));

                }

                if (!voice.isPlaying)
                    State = RoodkapjeState.Destroy;

                break;

            case RoodkapjeState.Destroy:

                break;
        }

        tState += Time.deltaTime;
    }

    void OnCollisionEnter (Collision col)
    {
        if (State != RoodkapjeState.Destroy && col.transform.tag == "Wolf")
        {
            print(transform.name + " was mauled.");
            State = RoodkapjeState.Dead;
        }
    }

    /*
     *  Public Functions
     */

    public void Init(RoodkapjeManager manager, Vector3 pos, Quaternion rot)
    {
        this.manager = manager;

        GetComponentInChildren<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        transform.position = pos;
        transform.rotation = rot;

        State = RoodkapjeState.Idle;

        fleeRadius = manager.fleeRadius;
        safeRadius = manager.safeRadius;
        deathRadius = manager.deathRadius;

        startleTime = Random.Range(0.25f, 0.5f);
    }

    public void SetRunningSpeed (float speed)
    {
        agent.speed = speed;
    }
}
