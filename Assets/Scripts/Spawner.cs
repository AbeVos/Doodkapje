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
        /*
         *  Dit is niet moeder's mooiste, oordeel niet :'(
         */

        float distance = Vector3.Distance(cam.position, transform.position);

        Vector3 pos3 = - Vector3.Normalize(cam.position - transform.position.normalized);
        Vector2 pos = new Vector2(pos3.x, pos3.z).normalized;
        Vector3 dir3 = cam.forward.normalized;
        Vector2 dir = new Vector2(dir3.x, dir3.z).normalized;

        Debug.DrawRay(Vector3.zero, dir);
        Debug.DrawRay(Vector3.zero, pos);

        float dot = Vector3.Dot(dir, pos);

        print(pos + ", " + dot);
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
