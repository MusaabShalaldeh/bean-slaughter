using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanDetector : EntityDetector
{
    [Header("Custom References")]
    public CombatManager combatManager;

    public override IEnumerator TargetingHandler()
    {
        while (!entity.isDead)
        {
            lastTarget = target;
            target = GetValidTarget();

            if (lastTarget != target) OnFirstTargetSight();

            yield return new WaitForSeconds(recheckTime);
        }
    }

    public override IEnumerator AttackChecker()
    {
        while (!entity.isDead)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= meleeRange
                    && combatManager.weaponState == CombatManager.WeaponState.melee)
                {
                    combatManager.MeleeAttack();
                }
                else if (!controller.IsMoving() && combatManager.weaponState == CombatManager.WeaponState.ranged)
                {
                    combatManager.RangedAttack(target.transform);
                }
            }

            yield return new WaitForSeconds(autoAttackTime);
        }
    }

    public override void OnFirstTargetSight()
    {

    }
}
