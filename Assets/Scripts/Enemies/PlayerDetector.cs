using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : EntityDetector
{
    [Header("Custom Referefences")]
    public EnemyCombatManager combatManager;

    EnemyController enemyController => (EnemyController)controller;

    public override IEnumerator TargetingHandler()
    {
        while (!entity.isDead)
        {
            lastTarget = target;
            target = GetValidTarget();

            if (lastTarget != target) OnFirstTargetSight();
            enemyController.CheckForPlayer(target);

            yield return new WaitForSeconds(recheckTime);
        }
    }

    public override IEnumerator AttackChecker()
    {
        while (!entity.isDead)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= meleeRange)
                {
                    combatManager.Attack(target.transform);
                    Debug.Log("attacking player in melee range!");
                }
                else if (!controller.IsMoving())
                {
                    combatManager.Attack(target.transform);
                    Debug.Log("attacking player in ranged range!");
                }
            }
            else
            {
                enemyController.PlayerEscaped();
            }

            yield return new WaitForSeconds(autoAttackTime);
        }
    }

    public override void OnFirstTargetSight()
    {
        
    }
}
