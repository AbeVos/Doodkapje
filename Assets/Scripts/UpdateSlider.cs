using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    public RoodkapjeManager rm;
    private Slider sl;

    void Start()
    {
        sl = GetComponent<Slider>();
    }

    void Update()
    {
        sl.value = rm.BloodLevel;
    }
}
