using UnityEngine;
using UnityStandardAssets.Utility;

[AddComponentMenu("Roodkapje/Wolf")]
public class Wolf : MonoBehaviour
{
    public float movementSpeed, sprintFactor, RotationSpeed;
    public AnimationCurve BobCurve;
    public GameObject camBoom;

    private CurveControlledBob cBob;


    // protected CapsuleCollider m_Collider;

    private void Start()
    {
        cBob = new CurveControlledBob();
        cBob.Setup(camBoom, 2);
        cBob.Bobcurve = BobCurve;
    }


    public void Move(Vector2 direction, bool sprint)
    {
        if (sprint)
        {
            transform.Translate((direction.x / 4) * (movementSpeed * sprintFactor), 0, direction.y * (movementSpeed * sprintFactor));
        }
        else
        {
            transform.Translate((direction.x / 4) * movementSpeed, 0, direction.y * movementSpeed);
        }
    }

    public void Rotate()
    {
        float nieseKanBlijkbaarWelLezen = transform.rotation.y + Input.GetAxis("Mouse X");
        transform.Rotate(0,  nieseKanBlijkbaarWelLezen, 0);
        /*
        float yaw = Camera.main.transform.localEulerAngles.y;
        if (yaw > 300)
        {
            yaw -= 360;
        }

        if (yaw < 10 && yaw > -10)
        {
            yaw /= 2.5f;
        }
        else if (yaw < 6 && yaw > -6)
        {
            yaw = 0f;
        }

        float rotYaw = RotationSpeed * yaw;
        transform.Rotate(0, rotYaw, 0);
        */
    }


    public void viewBob(float deltapos)
    {
      //  camBoom.transform.localPosition = cBob.DoHeadBob(deltapos);
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
