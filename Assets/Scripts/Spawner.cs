using UnityEngine;

[AddComponentMenu("Roodkapje/Spawner")]
public class Spawner : MonoBehaviour
{
    public enum Type
    {
        Roodkapje, Konijn
    }
    public Type type;

    public float margin = 5;
    public float cooldown = 20;

    private float timer;

    private RoodkapjeManager manager;

    private Camera cam;

	void Start ()
    {
        timer = cooldown;

        manager = FindObjectOfType<RoodkapjeManager>();

        cam = Camera.main;
	}
	
	void Update ()
    {
        /*
         *  Dit is niet moeder's mooiste, oordeel niet :'(
         */

        float distance = Vector3.Distance(cam.transform.position, transform.position);

        Vector3 pos3 = - Vector3.Normalize(cam.transform.position - transform.position.normalized);
        Vector2 pos = new Vector2(pos3.x, pos3.z).normalized;
        Vector3 dir3 = cam.transform.forward.normalized;
        Vector2 dir = new Vector2(dir3.x, dir3.z).normalized;

        float dot = Vector3.Dot(dir, pos);

        if (timer >= cooldown &&
            distance < cam.farClipPlane + margin &&
            distance > cam.farClipPlane &&
            dot > 0)
        {
            if (type == Type.Roodkapje)
            {
                manager.SpawnRoodkapje(transform.position);
            }
            else
            {
                manager.SpawnRabbit(transform.position);
            }

            timer = 0;
        }
        timer += Time.deltaTime;
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
