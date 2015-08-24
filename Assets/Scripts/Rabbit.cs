using UnityEngine;

[AddComponentMenu("Roodkapje/Rabbit")]
public class Rabbit : MonoBehaviour
{
    public Material[] materials;

    //  Geluiden komen nog wel

    /*public AudioClip[] gasps;
    public AudioClip[] walks;
    public AudioClip[] whimpers;
    public AudioClip[] shrieks;*/
    public AudioClip[] gore;    //  Dit geluid is er al

    public GameObject[] gibs;
    public Vector3[] gibPositions;

    public GameObject roodKwakje;

    private RoodkapjeManager manager;

    private float fleeRadius;
    private float safeRadius;
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
    //private AudioSource voice;

    /*
     *  State Machine
     */

    public enum RabbitState
    {
        Idle,       //  Nietsvermoedend is Rabbit bezig bloemen te plukken/een liedje te fluiten/een bolis te boetseren/...
        Startled,   //  De verschijning van de wolf doet Rabbit opschrikken
        Flee,       //  Rabbit rent weg van de wolf
        Dead,       //  Rabbit wordt verslonden door de wolf
        Destroy     //  De dode Rabbit wordt netjes opgeveegd
    }

    private RabbitState state;
    public RabbitState State
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

    void Start()
    {
        GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];

        wolf = FindObjectOfType<Wolf>();

        agent = GetComponent<NavMeshAgent>();

        /*voice = gameObject.AddComponent<AudioSource>();
        voice.playOnAwake = false;
        voice.pitch = Random.Range(0.85f, 1.25f);
        voice.spatialBlend = 1;*/

        feet = gameObject.AddComponent<AudioSource>();
        feet.playOnAwake = false;
        feet.pitch = Random.Range(0.85f, 1.15f);
        feet.spatialBlend = 1;

        State = RabbitState.Idle;
    }

    void Update()
    {
        wolfPos = wolf.transform.position;
        _distance = Vector3.Distance(wolfPos, transform.position);

        if (_distance > manager.safeRadius) return;

        switch (State)
        {
            case RabbitState.Idle:

                if (_distance <= fleeRadius) //  Dat konijn ook: ziet hij je niet van voren, dan hoort hij je wel met zijn oren.
                {
                    State = RabbitState.Startled;

                    //voice.PlayOneShot(gasps[Random.Range(0, gasps.Length)], 1.5f);
                }
                else if (tState >= Random.Range(2, 4))
                {
                    agent.SetDestination(transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
                }

                break;

            case RabbitState.Startled:

                if (tState >= startleTime)
                {
                    State = RabbitState.Flee;

                    //voice.clip = whimpers[Random.Range(0, whimpers.Length)];
                    //voice.Play();
                }

                break;

            case RabbitState.Flee:

                agent.SetDestination(transform.position + (transform.position - wolfPos) / _distance * safeRadius);

                if (_distance >= safeRadius)
                {
                    agent.SetDestination(transform.position);

                    State = RabbitState.Idle;
                }

                /*if (!feet.isPlaying)
                {
                    feet.clip = walks[Random.Range(0, walks.Length)];
                    feet.Play();
                }

                if (!voice.isPlaying && tState % 4 < Time.deltaTime)
                {
                    voice.clip = whimpers[Random.Range(0, whimpers.Length)];
                    voice.Play();
                }*/

                break;

            case RabbitState.Dead:
                if (tState == 0)
                {
                    GetComponentInChildren<Renderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;

                    manager.BloodLevel += 2;

                    for (int i = 0; i < gibs.Length; i++)
                    {
                        GameObject g = (GameObject)Instantiate(gibs[i], gibPositions[i] + transform.position, transform.rotation);
                    }

                    /*voice.clip = shrieks[Random.Range(0, shrieks.Length)];
                    voice.Play();*/

                    feet.clip = gore[Random.Range(0, gore.Length)];
                    feet.Play();

                    Instantiate(roodKwakje, transform.position, Quaternion.identity);   //  Want mathijs wilde meer impact voor een ontploffende roodkap

                }

                //if (!voice.isPlaying)
                    State = RabbitState.Destroy;

                break;

            case RabbitState.Destroy:

                break;
        }

        tState += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (State != RabbitState.Destroy && col.transform.tag == "Wolf")
        {
            print(transform.name + " was mauled.");
            State = RabbitState.Dead;
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
        GetComponent<NavMeshAgent>().enabled = true;

        transform.position = pos;
        transform.rotation = rot;

        State = RabbitState.Idle;

        fleeRadius = manager.fleeRadius;
        safeRadius = manager.safeRadius;
        startleTime = Random.Range(0.25f, 0.5f);
    }

    public void SetRunningSpeed(float speed)
    {
        agent.speed = speed;
    }
}
