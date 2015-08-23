using UnityEngine;

[AddComponentMenu("Roodkapje/Wolf")]
public class Wolf : MonoBehaviour
{
    public float movementSpeed, sprintFactor, RotationSpeed;
    public Animator anim;
    private Rigidbody rb;


    void Start()
    {
       // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // protected CapsuleCollider m_Collider;

    public void move(Vector2 direction, bool sprint)
    {
        if (sprint)
        {
           rb.velocity = sprintFactor * direction.y * 10 * transform.forward + sprintFactor * direction.x * 5 * transform.right + GetComponent<Rigidbody>().velocity.y * Vector3.one / 4;
        }
        else
        {
            rb.velocity = direction.y * 10 * transform.forward + direction.x * 5 * transform.right + GetComponent<Rigidbody>().velocity.y * Vector3.one / 4;
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    public void Rotate()
    {
        float yaw = Camera.main.transform.eulerAngles.y;

        float angle = Mathf.LerpAngle(transform.localEulerAngles.y, yaw, 0.1f);
        transform.localEulerAngles = transform.up * angle;
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
