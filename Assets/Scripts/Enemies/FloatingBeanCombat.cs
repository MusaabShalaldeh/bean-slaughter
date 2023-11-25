using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBeanCombat : EnemyCombatManager
{
    public Gun Mouth;
    public EnemyController enemyController;

    [Header("Settings")]
    public bool stopWhenAttackingPlayer = true;

    void OnEnable()
    {
        enemyController.stopWhenAttacking = stopWhenAttackingPlayer;
    }

    public override void Attack(Transform target)
    {
        Mouth.Shoot(target, () => {
            animator.PlayAttackAnimation();
        });
    }
}
