using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents : MonoBehaviour
{
    public MeleeWeapon Hammer;

    public void EnableWeapon()
    {
        Hammer.isActive = true;
    }

    public void DisableWeapon()
    {
        Hammer.isActive = false;
    }
}
