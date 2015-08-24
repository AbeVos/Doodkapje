using UnityEngine;
using UnityEngine.UI;

public class GetKapjes : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Text tx = GetComponent<Text>();
        string kp = "";

        try
        {
            kp = PlayerPrefs.GetInt("Kapjes").ToString();
        }
        catch (PlayerPrefsException e)
        {
            Debug.LogError(e);
            tx.text = "Many " + tx.text;
        }

        tx.text = kp + " " + tx.text;
    }

}
