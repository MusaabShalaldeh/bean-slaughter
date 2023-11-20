using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : Trigger
{
    [Header("References")]
    public CombatManager combatManager;

    public override void OnStartTouch(Collider obj)
    {

    }

    public override void OnEndTouch(Collider obj)
    {
        
    }

    public override void OnStay(Collider obj)
    {
        if (obj.tag != targetTag) return;

        combatManager.MeleeAttack();
    }
}
