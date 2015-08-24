using UnityEngine;
using System.Collections;

[AddComponentMenu("Roodkapje/Groeibloem")]
public class Groeibloem : MonoBehaviour
{
    //  Poem tie doem, ik ben een groeibloem

    public float groeiduur = 1.5f;

    private float t = 0;
	
	void Update ()
    {
        if (t < 1)
            t += Time.deltaTime / groeiduur;

        transform.localScale = Vector3.one * Mathf.Lerp(0, 1, t);
	}
}
