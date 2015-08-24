using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Roodkapje/Roodkapje Manager")]
public class RoodkapjeManager : MonoBehaviour
{
    public float fleeRadius = 10;
    public float safeRadius = 30;
    public float bloodDifficulty = 1.5f;

    public GameObject roodkapje;
    public GameObject rabbit;

    public AudioClip musicCreepy;
    public AudioClip musicHappy;

    private AudioSource music;

    private Wolf wolf;

    private List<GameObject> inUseRoodkapje;
    private List<GameObject> availableRoodkapje;

    private List<GameObject> inUseRabbit;
    private List<GameObject> availableRabbit;

    private float _blood;
    public float BloodLevel
    {
        get { return _blood; }
        set
        {
            if (_blood > 100)
            {
                _blood = 100;
            }
            else
            {
                _blood = value;
            }
        }
    }

    private int _kapjes;
    public int Kapjes
    {
        set
        {
            _kapjes = value;
        }
        get { return _kapjes; }
    }

    void Start ()
    {
        //  Spawn Roodkapjes
        inUseRoodkapje = new List<GameObject>();
        availableRoodkapje = new List<GameObject>(10);

        for (int i = 0; i < 10; i++)
        {
            SpawnRoodkapje(Random.Range(-30, 30), 0, Random.Range(-30, 30));
        }

        //  Sapwn Rabbits
        inUseRabbit = new List<GameObject>();
        availableRabbit = new List<GameObject>(10);

        for (int i = 0; i < 10; i++)
        {
            SpawnRabbit(Random.Range(-30, 30), 0, Random.Range(-30, 30));
        }

        music = gameObject.AddComponent<AudioSource>();
        music.loop = true;
        PlayCreepyMusic();

        wolf = FindObjectOfType<Wolf>();

        _blood = 100;
    }

    void Update ()
    {
        /*
         *  Roodkapje
         */

        bool roodkaploos = true;    //  Blijft true zolang er geen Roodkappen in de buurt zijn

        for (int i = 0; i < inUseRoodkapje.Count; i++)   //  Update Roodkapje pool
        {
            //  Pas rensnelheid aan aan het aantal aan stukken gescheurde Roodkapjes, wat een leuke aangelegenheid
            inUseRoodkapje[i].GetComponent<Roodkapje>().SetRunningSpeed(Kapjes / 15 + 5);

            if (inUseRoodkapje[i].GetComponent<Roodkapje>().State == Roodkapje.RoodkapjeState.Destroy)
            {
                availableRoodkapje.Add(inUseRoodkapje[i]);
                inUseRoodkapje.RemoveAt(i);
            }
            else if (roodkaploos && inUseRoodkapje[i].GetComponent<Roodkapje>().Distance < safeRadius)
            {
                PlayHappyMusic();

                roodkaploos = false;
            }
        }

        if (roodkaploos) PlayCreepyMusic();

        /*
         *  Rabbit
         */
         
        for (int i = 0; i < inUseRabbit.Count; i++)   //  Update Rabbit pool
        {
            //  Pas rensnelheid aan aan het aantal aan stukken gescheurde Konijnen, wat een leuke aangelegenheid
            //inUseRabbit[i].GetComponent<Rabbit>().SetRunningSpeed(Kapjes / 15 + 5);

            if (inUseRabbit[i].GetComponent<Rabbit>().State == Rabbit.RabbitState.Destroy)
            {
                availableRabbit.Add(inUseRabbit[i]);
                inUseRabbit.RemoveAt(i);
            }
        }

#if UNITY_EDITOR 
        //  Debug/test info

        if (Input.GetKey(KeyCode.Space))
        {
            print("In use: " + inUseRoodkapje.Count + ", Available: " + availableRoodkapje.Count);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnRoodkapje(wolf.transform.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
            }
        }
#endif

        _blood -= Time.deltaTime * bloodDifficulty;

        if (_blood <= 0)
        {
            SaveKapjes();
            // zie boven
        }
    }

    /*
     *  Custom Functions
     */

    void SaveKapjes ()
    {
        try
        {
            PlayerPrefs.SetInt("Kapjes", _kapjes);
        }
        catch (PlayerPrefsException e)
        {
            Debug.LogError(e);
        }
        finally
        {
            Application.LoadLevel("GameOver");
        }
    }


    void SpawnRoodkapje (Vector3 pos)
    {
        if (availableRoodkapje.Count > 0)
        {
            GameObject r = availableRoodkapje[availableRoodkapje.Count - 1];
            r.GetComponent<Roodkapje>().Init(this, pos, Quaternion.identity);

            inUseRoodkapje.Add(r);
            availableRoodkapje.Remove(r);
        }
        else
        {
            GameObject r = Instantiate(roodkapje);
            r.GetComponent<Roodkapje>().Init(this, pos, Quaternion.identity);

            inUseRoodkapje.Add(r);
        }
    }

    void SpawnRoodkapje (float x, float y, float z)
    {
        SpawnRoodkapje(new Vector3(x, y, z));
    }

    void SpawnRabbit (Vector3 pos)
    {
        if (availableRabbit.Count > 0)
        {
            GameObject r = availableRabbit[availableRabbit.Count - 1];
            r.GetComponent<Rabbit>().Init(this, pos, Quaternion.identity);

            inUseRabbit.Add(r);
            availableRabbit.Remove(r);
        }
        else
        {
            GameObject r = Instantiate(rabbit);
            r.GetComponent<Rabbit>().Init(this, pos, Quaternion.identity);

            inUseRabbit.Add(r);
        }
    }

    void SpawnRabbit (float x, float y, float z)
    {
        SpawnRabbit(new Vector3(x, y, z));
    }

    public void PlayHappyMusic ()
    {
        if (music.clip != musicHappy)
        {
            music.Stop();
            music.clip = musicHappy;
            music.Play();
        }
    }

    public void PlayCreepyMusic ()
    {
        if (music.clip != musicCreepy)
        {
            music.Stop();
            music.clip = musicCreepy;
            music.Play();
        }
    }
}
