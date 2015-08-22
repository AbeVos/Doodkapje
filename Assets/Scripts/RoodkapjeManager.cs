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

    private Wolf wolf;

    private List<GameObject> inUse;
    private List<GameObject> available;

    private float _blood;

    public float BloodLevel
    {
        get { return _blood; }
    }

    void Start()
    {
        inUse = new List<GameObject>();
        available = new List<GameObject>(10);

        for (int i = 0; i < 10; i++)
        {
            Spawn(Random.Range(-30, 30), 0, Random.Range(-30, 30));
        }

        wolf = FindObjectOfType<Wolf>();

        _blood = 100;
    }

    void Update()
    {
        for (int i = 0; i < inUse.Count; i++)
        {
            if (inUse[i].GetComponent<Roodkapje>().State == Roodkapje.RoodkapjeState.Destroy)
            {
                available.Add(inUse[i]);
                inUse.RemoveAt(i);
            }
        }

#if UNITY_EDITOR 
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
        _blood -= Time.deltaTime / bloodDifficulty;

        if (_blood <= 0)
        {
            Application.LoadLevel("GameOver");
        }
    }



    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 400), "Blood-o-meter: " + _blood + "%");
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

    public void AddBlood(float amount)
    {
        _blood += amount;

        if (_blood > 100) _blood = 100;
    }
}
