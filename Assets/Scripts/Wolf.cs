using UnityEngine;

[AddComponentMenu("Roodkapje/Wolf")]
public class Wolf : MonoBehaviour
{
    public float movementSpeed, sprintFactor, RotationSpeed;
    public Animator anim;
    private Rigidbody rb;
    private float newAngle, OldAngle;

    void Start()
    {
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        OldAngle = 0;
    }

    // protected CapsuleCollider m_Collider;

    public void move(Vector2 direction, bool sprint)
    {
        if (sprint)
        {
            rb.velocity = (transform.right * movementSpeed * direction.x)  +  (transform.forward * movementSpeed * sprintFactor * direction.y);
        }
        else
        {
            rb.velocity = (transform.right * movementSpeed * direction.x) + (transform.forward * movementSpeed *  direction.y);
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);

    }

    public void Rotate()
    {
        float yaw = Camera.main.transform.eulerAngles.y;

        float angle = Mathf.LerpAngle(transform.localEulerAngles.y, yaw, 0.1f);
        newAngle = angle;
        transform.localEulerAngles = transform.up * angle;

        anim.SetFloat("TailX", Mathf.Clamp(newAngle - OldAngle, -1, 1));
        OldAngle = newAngle;
    }

    public void PrimaryAttack()
    {
        Debug.Log("Primary Attack");
    }

    public void SecondaryAttack()
    {
        Debug.Log("Secondary Attack");
    }
}
