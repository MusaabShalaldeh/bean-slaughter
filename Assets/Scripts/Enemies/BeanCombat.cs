using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanCombat : EnemyCombatManager
{
    public MeleeWeapon HeadButt;

    public override void Attack(Transform target)
    {
        HeadButt.Swing(() => {
            animator.PlayAttackAnimation();
        });
    }
}
