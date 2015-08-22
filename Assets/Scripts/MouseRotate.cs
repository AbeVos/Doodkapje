using UnityEngine;

public class MouseRotate : MonoBehaviour
{
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        transform.eulerAngles += Vector3.up * Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
	}
}
