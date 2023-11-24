using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class EntityDetector : MonoBehaviour
{
    [Header("References")]
    public Entity entity;
    public EntityController controller;

    [Header("Settings")]
    public LayerMask targetLayer;
    public float recheckTime = 0.05f;
    public float autoAttackTime = 0.5f;
    public float meleeRange = 1.5f;
    public float maxRange = 6.0f;
    public bool debugMode = true;

    // Private Variables
    [HideInInspector] public Entity target;
    [HideInInspector] public Entity lastTarget;

    void Start()
    {
        StartCoroutine(TargetingHandler());
        StartCoroutine(AttackChecker());
    }

    void Update()
    {
        LockOnTarget();
    }

    public abstract IEnumerator AttackChecker();

    public abstract void OnFirstTargetSight();

    void LockOnTarget()
    {
        if (target == null || entity.isDead)
            return;

        if (!controller.IsMoving()) 
        {
            Vector3 direction = target.transform.position - entity.transform.position;
            direction.y = 0;

            entity.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public abstract IEnumerator TargetingHandler();

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

    bool InSight(Transform entity, Transform target)
    {
        Vector3 direction = target.position - entity.position;
        Vector3 pos = new Vector3(entity.position.x, entity.position.y + 0.5f, entity.position.z);

        if (Physics.Raycast(pos, direction, out RaycastHit hit))
        {
            if (hit.transform == target)
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