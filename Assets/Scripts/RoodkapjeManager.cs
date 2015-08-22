using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Roodkapje/Roodkapje Manager")]
public class RoodkapjeManager : MonoBehaviour
{
    public float fleeRadius = 10;
    public float safeRadius = 30;

    public GameObject prefab;

	void Start ()
    {
        for (int i = 0; i < 5; i++)
        {
            Spawn(Random.Range(-30, 30), 0, Random.Range(-30, 30));
        }
	}

    void Spawn(Vector3 pos)
    {
        GameObject r = (GameObject) Instantiate(prefab, pos, Quaternion.identity);
        r.GetComponent<Roodkapje>().Init(this);
    }

    void Spawn(float x, float y, float z)
    {
        Spawn(new Vector3(x, y, z));
    }
}
