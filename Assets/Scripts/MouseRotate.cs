using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    private float angle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        angle = 0;
    }

    void Update()
    {
        angle += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);

        angle = Mathf.Clamp(angle, -40, 40);
        angle = Mathf.Lerp(angle, 0, 0.01f);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);

        print(transform.localEulerAngles.y);
    }
}