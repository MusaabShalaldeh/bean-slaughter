using UnityEngine;

public abstract class EnemyCombatManager : MonoBehaviour
{
    [Header("References")]
    public EnemyAnimator animator;

    public abstract void Attack(Transform target);
}
