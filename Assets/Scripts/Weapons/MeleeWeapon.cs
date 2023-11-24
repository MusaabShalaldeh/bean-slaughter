using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("References")]
    public AudioSource SoundSource;

    [Header("Sound")]
    public AudioClip SwingSFX;

    [Header("Graphic Effect")]
    public GameObject ImpactEffect;

    [Header("Settings")]
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

        SpawnImpactEffectAtCollisionPoint(ImpactEffect, impactPoint);

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

    public void SpawnImpactEffectAtCollisionPoint(GameObject effect, Vector3 position)
    {
        if (ImpactEffect == null) return;

        GameObject spawnedEffect = Instantiate(ImpactEffect, position, Quaternion.Euler(0, 0, 0));
        Destroy(spawnedEffect, 0.6f);
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
