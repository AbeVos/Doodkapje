using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public float extarMouse, deadZone;
    private float angleX, angleY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        angleX = 0;
        angleY = 35;
    }

    void Update()
    {
        angleX += Input.GetAxis("Mouse X") * extarMouse;
        angleY -= Input.GetAxis("Mouse Y") / 1.2f;

        angleX = Mathf.Clamp(angleX, -40, 40);
        angleY = Mathf.Clamp(angleY, -20, 55);

        angleX = Mathf.Lerp(angleX, 0.2f, Time.deltaTime);
        angleY = Mathf.Lerp(angleY, 35f, Time.deltaTime * 1.5f);

        transform.localEulerAngles = new Vector3(angleY, angleX, transform.localEulerAngles.z);

    }
}