using UnityEngine;

[AddComponentMenu("Roodkapje/Blood Pickup")]
public class BloodPickup : MonoBehaviour
{
    private RoodkapjeManager manager;
    public int BloodReward = 5;
    private float timer;

    private Transform wolf;

    void Start ()
    {
        manager = FindObjectOfType<RoodkapjeManager>();

        wolf = FindObjectOfType<Wolf>().transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10 || Vector3.Distance(wolf.position, transform.position) > 20)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (timer >= 1 && col.transform.tag == "Wolf")
        {
            manager.BloodLevel += BloodReward;

            Destroy(gameObject);
        }
    }
}
