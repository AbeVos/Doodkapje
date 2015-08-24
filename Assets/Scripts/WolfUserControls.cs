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
        v = Mathf.Clamp(v, -.5f, 1);

        Vector2 dir = new Vector2(h / 3, v);
        WolfCharacter.move(dir, s);
        WolfCharacter.Rotate();

        if (Input.GetMouseButtonDown(0))
        {
            WolfCharacter.PrimaryAttack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            WolfCharacter.SecondaryAttack();
        }
    }

}
