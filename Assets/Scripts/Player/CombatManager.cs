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
    public PlayerAnimator pAnimator;

    // Private Variables
    [HideInInspector] public WeaponState weaponState;

    void Start()
    {
        SetWeaponState(WeaponState.melee);
    }

    public void MeleeAttack()
    {
        Hammer.Swing(() => {
            pAnimator.PlayMeleeAttackAnimation();
        });
    }

    public void RangedAttack(Transform target)
    {
        Pistol.Shoot(target, () => {
            pAnimator.PlayShootAnimation();
        });
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
        Pistol.gameObject.SetActive(false);
    }

    private void EnableRangedWeapon()
    {
        Pistol.gameObject.SetActive(true);
        Hammer.gameObject.SetActive(false);
        Hammer.isActive = false;
    }

    bool IntToBool(int n)
    {
        if (n == 0)
            return false;
        else
            return true;
    }
}
