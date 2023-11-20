using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("References")]
    public PlayerAnimator animator;

    [Header("Settings")]
    public float damage = 25.0f;
    public string targetTag = "Enemy";

    // Private Variables
    [SerializeField] public bool isActive = false;

    public void Swing()
    {
        if (isActive) return;

        animator.PlayMeleeAttackAnimation();
    }

    void OnTriggerStay(Collider other)
    {
        if (!isActive || other.tag != targetTag) 
            return;

        Entity target = other.GetComponent<Entity>();
        DealDamage(target);
    }

    void DealDamage(Entity target)
    {
        target.TakeDamage(damage);
        Debug.Log(target.entityName + " has taken " + damage.ToString() + " points of damage.");
    }
}
