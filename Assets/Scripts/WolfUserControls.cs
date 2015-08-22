using UnityEngine;

[AddComponentMenu("Roodkapje/WolfControlls")]
public class WolfUserControls : MonoBehaviour
{
    protected Wolf WolfCharacter;

    private void Start()
    {
        WolfCharacter = GetComponent<Wolf>();
    }

    private void FixedUpdate()
    {
        // Vector3 CamRotNew = Camera.main.transform.forward;
        //  Vector3 CamRotNew = WolfCharacter.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool s = Input.GetKey(KeyCode.LeftShift);

        Vector2 dir = new Vector2(h, v);
        WolfCharacter.move(dir, s);
        WolfCharacter.Rotate();

        if (Input.GetMouseButtonDown(0))
        {
            WolfCharacter.PrimaryAtack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            WolfCharacter.SecondaryAtack();
        }
        // CamRot = CamRotNew;
        // Debug.Log(Vector3.Angle(CamRotNew, CamRot)); 
    }

}
