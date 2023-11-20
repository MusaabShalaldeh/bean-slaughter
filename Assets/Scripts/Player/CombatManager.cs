using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    enum WeaponState
    {
        melee = 0,
        ranged = 1,
    }

    [Header("References")]
    public MeleeWeapon Hammer;

    // Private Variables
    WeaponState weaponState = WeaponState.melee;

    public void MeleeAttack()
    {
        Hammer.Swing();
    }
}
