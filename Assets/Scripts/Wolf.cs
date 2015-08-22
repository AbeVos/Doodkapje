using UnityEngine;

[AddComponentMenu("Roodkapje/Wolf")]
public class Wolf : MonoBehaviour
{
    public float movementSpeed, sprintFactor, RotationSpeed;

    // protected CapsuleCollider m_Collider;

    public void move(Vector2 direction, bool sprint)
    {
        if (sprint)
        {
            GetComponent<Rigidbody>().velocity = sprintFactor * direction.y * 10 * transform.forward + sprintFactor * direction.x * 5 * transform.right + GetComponent<Rigidbody>().velocity.y * Vector3.one;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = direction.y * 10 * transform.forward + direction.x * 5 * transform.right + GetComponent<Rigidbody>().velocity.y * Vector3.one;
        }

    }

    public void Rotate()
    {
        float yaw = Camera.main.transform.localEulerAngles.y;
        if (yaw > 300)
        {
            yaw -= 360;
        }

        float rotYaw = RotationSpeed * yaw;
        transform.Rotate(0, rotYaw, 0);
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
