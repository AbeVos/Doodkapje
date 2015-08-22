using UnityEngine;
using UnityEngine.UI;

public class GetKapjes : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        

        try
        {
            PlayerPrefs.GetInt("Kapjes");
        }
        catch (PlayerPrefsException e)
        {
            Debug.LogError(e);
        }
	}
	
}
