using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("References")]
    public GameObject Model;
    public AudioSource soundSource;

    [Header("Entity Data")]
    public string entityName = "Entity";
    public float maxHealth = 100;
    public float speed = 3;
    public float invinsibilityTime = 0.4f;

    [Header("Sound Effects")]
    public AudioClip DeathSFX;
    public AudioClip HitSFX;

    // Private Variables
    [SerializeField] float currentHealth;
    [HideInInspector] public bool isDead = false;
    bool hasTakenHit = false;

    void OnEnable()
    {
        Initiate();
    }

    public void Initiate()
    {
        currentHealth = maxHealth;
    }

    #region Health

    public void Heal(float amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHeal();
    }

    public void TakeDamage(float amount)
    {
        if (hasTakenHit) return;

        hasTakenHit = true;
        StartCoroutine(HitCooldown(invinsibilityTime));

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        OnDamageTaken();
    }

    IEnumerator HitCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        hasTakenHit = false;
    }

    public abstract void OnHeal();

    public abstract void OnDamageTaken();

    #endregion

    #region Death
    public void Die()
    {
        isDead = true;
        StartCoroutine(DieSequence());
    }

    public abstract IEnumerator DieSequence();

    #endregion
}
