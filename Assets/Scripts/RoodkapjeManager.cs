using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Roodkapje/Roodkapje Manager")]
public class RoodkapjeManager : MonoBehaviour
{
    public float fleeRadius = 10;
    public float safeRadius = 30;
    public float bloodDifficulty = 1.5f;

    public GameObject prefab;

    public AudioClip musicCreepy;
    public AudioClip musicHappy;

    private AudioSource music;

    private Wolf wolf;

    private List<GameObject> inUse;
    private List<GameObject> available;

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

    void Start()
    {
        inUse = new List<GameObject>();
        available = new List<GameObject>(10);

        for (int i = 0; i < 10; i++)
        {
            Spawn(Random.Range(-30, 30), 0, Random.Range(-30, 30));
        }

        music = gameObject.AddComponent<AudioSource>();
        music.loop = true;
        PlayCreepyMusic();

        wolf = FindObjectOfType<Wolf>();

        _blood = 100;
    }

    void Update()
    {
        bool roodkaploos = true;

        for (int i = 0; i < inUse.Count; i++)   //  Clean inUse List
        {
            inUse[i].GetComponent<Roodkapje>().SetRunningSpeed(Kapjes / 15 + 5);

            if (inUse[i].GetComponent<Roodkapje>().State == Roodkapje.RoodkapjeState.Destroy)
            {
                available.Add(inUse[i]);
                inUse.RemoveAt(i);
            }
            else if (roodkaploos && inUse[i].GetComponent<Roodkapje>().Distance < safeRadius)
            {
                PlayHappyMusic();

                roodkaploos = false;
            }
        }

        if (roodkaploos) PlayCreepyMusic();

#if UNITY_EDITOR 
        //  Debug/test info

        if (Input.GetKey(KeyCode.Space))
        {
            print("In use: " + inUse.Count + ", Available: " + available.Count);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < 10; i++)
            {
                Spawn(wolf.transform.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
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

    void SaveKapjes()
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


    void Spawn(Vector3 pos)
    {
        if (available.Count > 0)
        {
            GameObject r = available[available.Count - 1];
            r.GetComponent<Roodkapje>().Init(this, pos, Quaternion.identity);

            inUse.Add(r);
            available.Remove(r);
        }
        else
        {
            GameObject r = Instantiate(prefab);
            r.GetComponent<Roodkapje>().Init(this, pos, Quaternion.identity);

            inUse.Add(r);
        }
    }

    void Spawn(float x, float y, float z)
    {
        Spawn(new Vector3(x, y, z));
    }

    public void PlayHappyMusic()
    {
        if (music.clip != musicHappy)
        {
            music.Stop();
            music.clip = musicHappy;
            music.Play();
        }
    }

    public void PlayCreepyMusic()
    {
        if (music.clip != musicCreepy)
        {
            music.Stop();
            music.clip = musicCreepy;
            music.Play();
        }
    }
}
