using UnityEngine;
using System.Collections;

public class Autodestruct : MonoBehaviour
{
    public float time = 1;
	
	void Update ()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(gameObject);
        }
	}
}
