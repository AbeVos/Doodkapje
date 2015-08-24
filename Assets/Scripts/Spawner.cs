using UnityEngine;

[AddComponentMenu("Roodkapje/Spawner")]
public class Spawner : MonoBehaviour
{
    public enum Type
    {
        Roodkapje, Konijn
    }
    public Type type;

    private RoodkapjeManager manager;

    private Transform cam;

	void Start ()
    {
        manager = FindObjectOfType<RoodkapjeManager>();

        cam = Camera.main.transform;
	}
	
	void Update ()
    {
        float distance = Vector3.Distance(cam.position, transform.position);
	}

    void OnDrawGizmos()
    {
        switch (type)
        {
            case Type.Roodkapje:

                Gizmos.DrawWireCube(transform.position + Vector3.up / 2, Vector3.one / 2);

                break;

            case Type.Konijn:

                Gizmos.DrawWireSphere(transform.position + Vector3.up / 2, 1 / 2);

                break;
        }
    }
}
