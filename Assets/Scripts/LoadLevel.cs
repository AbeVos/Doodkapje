using UnityEngine;

public class LoadLevel : MonoBehaviour {

    public void Load(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
