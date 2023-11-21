using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public PlayerController controller;
    public CombatManager combatManager;

    [Header("Settings")]
    public LayerMask targetLayer;
    public float recheckTime = 0.05f;
    public float autoAttackTime = 0.5f;
    public float meleeRange = 1.5f;
    public float maxRange = 6.0f;
    public bool debugMode = true;

    // Private Variables
    [HideInInspector] public Entity target;
    Entity lastTarget;

    void Start()
    {
        StartCoroutine(TargetingHandler());
        StartCoroutine(AttackChecker());
    }

    void Update()
    {
        LockOnTarget();
    }

    IEnumerator AttackChecker()
    {
        while (!player.isDead)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= meleeRange 
                    && combatManager.weaponState == CombatManager.WeaponState.melee)
                {
                    combatManager.MeleeAttack();
                }
                else if(!controller.IsMoving() && combatManager.weaponState == CombatManager.WeaponState.ranged)
                {
                    combatManager.RangedAttack(target.transform);
                }
            }

            yield return new WaitForSeconds(autoAttackTime);
        }
    }

    void LockOnTarget()
    {
        if (target != null)
        {
            if (!controller.IsMoving()) player.transform.DOLookAt(target.transform.position, 0.1f);
        }
    }

    void OnFirstTargetSight()
    {

    }

    IEnumerator TargetingHandler()
    {
        while (!player.isDead)
        {
            lastTarget = target;
            target = GetValidTarget();

            if (lastTarget != target) OnFirstTargetSight();

            yield return new WaitForSeconds(recheckTime);
        }
    }

    public Entity GetValidTarget()
    {
        Collider[] candidates = Physics.OverlapSphere(transform.position, maxRange, targetLayer);
        Entity chosenTarget = null;

        if (candidates.Length > 0)
        {
            foreach (Collider candidate in candidates)
            {
                if (InSight(transform, candidate.transform))
                {
                    Entity potentialTarget = candidate.GetComponent<Entity>();

                    if (!potentialTarget.isDead)
                    {
                        if (chosenTarget == null)
                        {
                            chosenTarget = potentialTarget;
                        }
                        else
                        {
                            if (Vector3.Distance(transform.position, chosenTarget.transform.position) > Vector3.Distance(transform.position, candidate.transform.position))
                            {
                                chosenTarget = potentialTarget;
                            }
                        }
                    }
                }
            }
        }

        return chosenTarget;
    }

    bool InSight(Transform player, Transform enemy)
    {
        Vector3 direction = enemy.position - player.position;
        Vector3 pos = new Vector3(player.position.x, player.position.y + 0.5f, player.position.z);

        if (Physics.Raycast(pos, direction, out RaycastHit hit))
        {
            if (hit.transform == enemy)
            {
                if(debugMode) 
                    Debug.DrawLine(pos, hit.point, Color.yellow, 0.1f);

                return true;
            }
            else
                return false;
        }

        return false;
    }

}