using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents : MonoBehaviour
{
    [Header("References")]
    public MeleeWeapon Weapon;

    public void EnableWeapon()
    {
        Weapon.isActive = true;
    }

    public void DisableWeapon()
    {
        Weapon.isActive = false;
    }

    public void PlaySwingSound()
    {
        Weapon.PlaySwingSound();
    }
}
