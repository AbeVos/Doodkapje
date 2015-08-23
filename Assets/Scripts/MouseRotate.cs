using UnityEngine;

public class MouseRotate : MonoBehaviour
{
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        transform.localEulerAngles += Vector3.up * Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);

        float angle = transform.localEulerAngles.y;

        if (angle < 180)
        {
            angle = Mathf.Clamp(angle, 0, 40);
        }
        else
        {
            angle = Mathf.Clamp(angle, 320, 360);
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);

        print(transform.localEulerAngles.y);
	}
}
