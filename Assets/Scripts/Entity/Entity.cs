using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("References")]
    public GameObject Model;

    [Header("Entity Data")]
    public string entityName = "Entity";
    public float maxHealth = 100;
    public float speed = 3;

    // Private Variables
    [SerializeField] float currentHealth;
    [HideInInspector] public bool isDead = false;

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
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        OnDamageTaken();
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
