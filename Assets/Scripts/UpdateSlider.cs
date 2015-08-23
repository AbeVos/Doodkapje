using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    public RoodkapjeManager rm;
    private Image im;

    void Start()
    {
        im = GetComponent<Image>();
    }

    void Update()
    {
        im.fillAmount = rm.BloodLevel/100;
    }
}
