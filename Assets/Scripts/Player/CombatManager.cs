using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public enum WeaponState
    {
        melee = 0,
        ranged = 1,
    }

    [Header("References")]
    public MeleeWeapon Hammer;
    public Gun Pistol;
    public GameObject MeleeDetector;
    public PlayerAnimator pAnimator;

    // Private Variables
    WeaponState weaponState;

    void Start()
    {
        SetWeaponState(WeaponState.ranged);
    }

    public void MeleeAttack()
    {
        Hammer.Swing();
    }

    void SetWeaponState(WeaponState s)
    {
        weaponState = s;
        pAnimator.SetWeaponIdleAnimation(IntToBool((int)weaponState));

        switch (weaponState)
        {
            case WeaponState.melee:
                EnableMeleeWeapon();
                break;
            case WeaponState.ranged:
                EnableRangedWeapon();
                break;
        }
    }

    public void ToggleWeapons()
    {
        if (weaponState == WeaponState.melee)
            SetWeaponState(WeaponState.ranged);
        else
            SetWeaponState(WeaponState.melee);
    }

    private void EnableMeleeWeapon()
    {
        Hammer.gameObject.SetActive(true);
        MeleeDetector.SetActive(true);
        Pistol.gameObject.SetActive(false);
    }

    private void EnableRangedWeapon()
    {
        Pistol.gameObject.SetActive(true);
        MeleeDetector.SetActive(false);
        Hammer.gameObject.SetActive(false);
    }

    bool IntToBool(int n)
    {
        if (n == 0)
            return false;
        else
            return true;
    }
}
