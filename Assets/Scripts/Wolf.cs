using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[AddComponentMenu("Roodkapje/Wolf")]
public class Wolf : ThirdPersonCharacter
{
    public void PrimaryAtack()
    {
        Debug.Log("Primary Atack");
    }

    public void SecondaryAtack()
    {
        Debug.Log("Secondary Atack");
    }
}
