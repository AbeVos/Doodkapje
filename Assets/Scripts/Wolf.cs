using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("Roodkapje/Wolf")]
public class Wolf : MonoBehaviour
{
    public float movementSpeed, sprintFactor, RotationSpeed;
    public Animator anim;
    public Image SprintBar;
    public SphereCollider sc;

    private Camera cam;
    private VignetteAndChromaticAberration vaca;

    private Rigidbody rb;
    private float newAngle, OldAngle, fov, fade, SprintMeterTimer;
    private int sprintTimer;
    private bool isSprinting;

    void Start()
    {
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        OldAngle = 0;
        SprintMeterTimer = 1f;

        cam = Camera.main;
        fov = cam.fieldOfView;
        vaca = cam.GetComponent<VignetteAndChromaticAberration>();
    }

    public void Sprint()
    {
        if (!isSprinting)
        {
            Debug.Log("StartingSprint");
            isSprinting = true;
            sprintTimer = 3;
            Debug.Log(sprintTimer);
            StartCoroutine("SprintRoutine");
            StartCoroutine("StartSprint");
            fade = 0f;
        }

    }
    IEnumerator StartSprint()
    {
        LerpFX(fade);
        fade += 0.1f;
        yield return null;
        if (fade <= 1) StartCoroutine("StartSprint");
    }

    IEnumerator EndSprint()
    {
        LerpFX(fade);
        fade -= 0.1f;
        yield return null;
        if (fade >= 0) StartCoroutine("EndSprint");
    }
    IEnumerator SprintRoutine()
    {
        if (sprintTimer <= 0)
        {
            isSprinting = false;
            StartCoroutine("EndSprint");
            SprintMeterTimer = 1f;
        }
        else
        {
            sprintTimer -= 1;
            SprintMeterTimer -= (1f/3f);
            yield return new WaitForSeconds(1f);
            StartCoroutine("SprintRoutine");
        }
        
    }

    private void LerpFX(float alpha)
    {
        vaca.chromaticAberration = Mathf.Lerp(1f, 8f, alpha);
        vaca.blur = Mathf.Lerp(1.7f, 1.2f, alpha);
        vaca.intensity = Mathf.Lerp(3f, 4.5f, alpha);
        cam.fieldOfView = Mathf.Lerp(fov, fov * 1.3f, alpha);
    }

    public void move(Vector2 direction)
    {
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector3.down);

        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);

        if (!Physics.Raycast(ray, 1))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity += Physics.gravity;
            }
            print("Falling");
            
            return;
        }

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (isSprinting)
        {
            rb.velocity += (transform.right * movementSpeed * direction.x) /*+ Physics.gravity*/ + (transform.forward * movementSpeed * sprintFactor * direction.y);
        }
        else
        {
            rb.velocity += (transform.right * movementSpeed * direction.x) /*+ Physics.gravity*/ + (transform.forward * movementSpeed * direction.y);
            //rb.AddForce(1 * (transform.right * movementSpeed * direction.x + transform.forward * movementSpeed * direction.y));
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);

    }

    private void Update()
    {
        SprintBar.fillAmount = SprintMeterTimer;
    }

    public void Rotate()
    {
        float yaw = Camera.main.transform.eulerAngles.y;

        float angle = Mathf.LerpAngle(transform.localEulerAngles.y, yaw, Time.deltaTime * RotationSpeed);
        newAngle = angle;
        transform.localEulerAngles = transform.up * angle;

        anim.SetFloat("TailX", Mathf.Clamp(newAngle - OldAngle, -1, 1));
        OldAngle = newAngle;
    }

    public void PrimaryAttack()
    {
        sc.enabled = true;
        anim.SetTrigger("Attack");
        sc.enabled = false;
    }

    public void SecondaryAttack()
    {
        Debug.Log("Secondary Attack");
    }
}
