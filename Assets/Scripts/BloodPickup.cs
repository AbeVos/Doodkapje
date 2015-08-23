using UnityEngine;
using System.Collections;

[AddComponentMenu("Roodkapje/Blood Pickup")]
public class BloodPickup : MonoBehaviour
{
    private RoodkapjeManager manager;
    public int BloodReward = 5;
    private float timer;

    void Start ()
    {
        manager = FindObjectOfType<RoodkapjeManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
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
