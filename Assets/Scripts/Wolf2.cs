using UnityEngine;

public class Wolf2 : MonoBehaviour
{
    public float forwardSpeed = 2;

    public float maxSpeed = 5;
    
    private Rigidbody rb;

    public Transform pivot;

	void Start ()
    {
        //contr = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        Vector3 acceleration = new Vector3();

        acceleration.z += Input.GetAxis("Vertical") / 2;

        //contr.SimpleMove(direction);
        rb.velocity = new Vector3(0, 0, Mathf.Lerp(rb.velocity.z, Input.GetAxis("Vertical") * forwardSpeed, 0.5f));
	}
}
