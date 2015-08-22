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

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 3), "Press Space to Start.");
    }
}
