using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class WolfUserControls : ThirdPersonUserControl
{
    protected Wolf m_WolfCharacter;

    private void Start()
    {
        m_WolfCharacter = GetComponent<Wolf>();
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
        if(Input.GetMouseButtonDown(0))
        {
            m_WolfCharacter.PrimaryAtack();
        }

    }

}
