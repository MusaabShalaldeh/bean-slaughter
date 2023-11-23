using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents : MonoBehaviour
{
    [Header("References")]
    public MeleeWeapon Weapon;

    public void EnableWeapon()
    {
        Weapon.ActivateWeapon();
    }

    public void DisableWeapon()
    {
        Weapon.DeactivateWeapon();
    }

    public void PlaySwingSound()
    {
        Weapon.PlaySwingSound();
    }
}
