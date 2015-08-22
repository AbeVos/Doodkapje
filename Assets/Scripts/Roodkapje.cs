﻿using UnityEngine;

[AddComponentMenu("Roodkapje/Roodkapje")]
public class Roodkapje : MonoBehaviour
{
    public AudioClip[] gasps;
    public AudioClip[] walks;
    public AudioClip[] whimpers;
    public AudioClip[] shrieks;
    public AudioClip[] gore;

    public GameObject[] gibs;
    public Vector3[] gibPositions;

    private RoodkapjeManager manager;

    private float fleeRadius;
    private float safeRadius;
    private float startleTime;

    private Wolf wolf;
    private Vector3 wolfPos;
    private float distance;

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
        voice.pitch = Random.Range(0.85f, 1.15f);

        feet = gameObject.AddComponent<AudioSource>();
        feet.playOnAwake = false;
        feet.pitch = Random.Range(0.85f, 1.15f);

        State = RoodkapjeState.Idle;
	}
	
	void Update ()
    {
        wolfPos = wolf.transform.position;
        distance = Vector3.Distance(wolfPos, transform.position);

        switch (State)
        {
            case RoodkapjeState.Idle:

                if (distance <= fleeRadius)
                {
                    State = RoodkapjeState.Startled;
                    
                    voice.PlayOneShot(gasps[Random.Range(0, gasps.Length)], 1.5f);
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

                agent.SetDestination(transform.position + (transform.position - wolfPos) / distance * safeRadius);

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

                if (distance >= safeRadius)
                {
                    agent.SetDestination(transform.position);

                    State = RoodkapjeState.Idle;
                }

                break;

            case RoodkapjeState.Dead:
                if (tState == 0)
                {
                    GetComponent<Renderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;


                    for (int i = 0; i < gibs.Length; i++)
                    {
                        GameObject g = (GameObject)Instantiate(gibs[i], gibPositions[i] + transform.position, transform.rotation);
                        g.GetComponent<Rigidbody>().AddExplosionForce(10, transform.position, 10, 1, ForceMode.Impulse);
                    }

                    voice.clip = shrieks[Random.Range(0, shrieks.Length)];
                    voice.Play();

                    feet.clip = gore[Random.Range(0, gore.Length)];
                    feet.Play();
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

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;

        transform.position = pos;
        transform.rotation = rot;

        State = RoodkapjeState.Idle;

        fleeRadius = manager.fleeRadius;
        safeRadius = manager.safeRadius;
        startleTime = Random.Range(0.25f, 0.5f);
    }
}
