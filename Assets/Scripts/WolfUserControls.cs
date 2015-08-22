using UnityEngine;

[AddComponentMenu("Roodkapje/WolfControlls")]
public class WolfUserControls : MonoBehaviour
{
    protected Wolf WolfCharacter;
    protected Vector3 deltapos, oldPos, newPos;

    private void Start()
    {
        WolfCharacter = GetComponent<Wolf>();
        oldPos = transform.position;
        newPos = oldPos;
    }

    private void FixedUpdate()
    {
        newPos = transform.position;
        deltapos = newPos - oldPos;
        // Vector3 CamRotNew = Camera.main.transform.forward;
        //  Vector3 CamRotNew = WolfCharacter.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool s = Input.GetKey(KeyCode.LeftShift);

        Vector2 dir = new Vector2(h, v);
        WolfCharacter.Move(dir, s);
        WolfCharacter.Rotate();
        WolfCharacter.viewBob(deltapos.magnitude);

        if (Input.GetMouseButtonDown(0))
        {
            WolfCharacter.PrimaryAttack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            WolfCharacter.SecondaryAttack();
        }

        Debug.Log(deltapos.magnitude);
        oldPos = newPos;
    }

}
