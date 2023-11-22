using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents : MonoBehaviour
{
    public MeleeWeapon Weapon;

    public void EnableWeapon()
    {
        Weapon.isActive = true;
    }

    public void DisableWeapon()
    {
        Weapon.isActive = false;
    }
}
