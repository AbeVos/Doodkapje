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
            transform.Translate(direction.x * ((movementSpeed * sprintFactor) / 10), 0, direction.y * ((movementSpeed * sprintFactor) / 10));
        }
        else
        {
            transform.Translate(direction.x * (movementSpeed / 10), 0, direction.y * (movementSpeed / 10));
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

    public void PrimaryAtack()
    {
        Debug.Log("Primary Atack");
    }

    public void SecondaryAtack()
    {
        Debug.Log("Secondary Atack");
    }
}
