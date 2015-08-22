using UnityEngine;
using System.Collections;

public class MouseRotation : MonoBehaviour
{
    
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
        //transform.localPosition += new Vector3(Mathf.Lerp(0, Input.GetAxis("Mouse X"), 0.5f), 0, 0);
        //float maxRadius = 2.5f * Mathf.Tan(Camera.main.fieldOfView);
        //transform.localPosition = new Vector3(Mathf.Clamp(transform.position.x, - maxRadius, maxRadius), 0, 0);

        //Camera.main.transform.LookAt(transform);
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
