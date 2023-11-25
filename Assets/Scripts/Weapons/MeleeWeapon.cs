using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public enum ImpactEffect
    {
        bam = 0,
        blood = 1,
    }

    [Header("References")]
    public AudioSource SoundSource;

    [Header("Sound")]
    public AudioClip SwingSFX;

    [Header("Settings")]
    public ImpactEffect impactEffect;
    public float damage = 25.0f;
    public string targetTag = "Enemy";
    public float enemyHeadOffset = 0.8f;

    // Private Variables
    [SerializeField] public bool isActive = false;
    Collider WeaponCollider;

    private void Start()
    {
        WeaponCollider = GetComponent<Collider>();
    }

    public void Swing(Action AttackAnimation)
    {
        if (isActive) return;

        AttackAnimation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive || other.tag != targetTag) 
            return;

        //isActive = false;

        Entity target = other.GetComponent<Entity>();
        Vector3 impactPoint = new Vector3(target.transform.position.x, target.transform.position.y + enemyHeadOffset, target.transform.position.z);

        SpawnImpactEffectAtCollisionPoint(impactPoint);

        DealDamage(target);
    }

    void DealDamage(Entity target)
    {
        target.TakeDamage(damage);
        // Debug.Log(target.entityName + " has taken " + damage.ToString() + " points of damage.");
    }

    public void PlaySwingSound()
    {
        if (SwingSFX == null || SoundSource == null)
            return;
        
        SoundSource.PlayOneShot(SwingSFX);
    }

    public void SpawnImpactEffectAtCollisionPoint(Vector3 position)
    {
        GameObject spawnedEffect;

        switch (impactEffect)
        {
            case ImpactEffect.bam:
                spawnedEffect = ObjectPool.instance.GetObject(ObjectPool.ObjectTypes.hitEffect, position);
                if (spawnedEffect != null)
                    ObjectPool.instance.ReturnObject(spawnedEffect, ObjectPool.ObjectTypes.hitEffect, 0.6f);
                break;
            case ImpactEffect.blood:
                spawnedEffect = ObjectPool.instance.GetObject(ObjectPool.ObjectTypes.bloodHitEffect, position);
                if (spawnedEffect != null)
                    ObjectPool.instance.ReturnObject(spawnedEffect, ObjectPool.ObjectTypes.bloodHitEffect, 0.6f);
                break;
        }
    }

    public void ActivateWeapon()
    {
        WeaponCollider.enabled = true;
        isActive = true;
    }

    public void DeactivateWeapon()
    {
        WeaponCollider.enabled = false;
        isActive = false;
    }
}
