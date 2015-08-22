using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class MainMenu : MonoBehaviour
{
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
}
