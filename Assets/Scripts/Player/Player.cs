using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #region singleton
    public static Player instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("References")]
    public PlayerAnimator playerAnimator;
    public PlayerController playerController;
    public HealthBar healthBar;
    public List<GameObject> ObjectsToDisableOnDeath;

    void Start()
    {
        healthBar.Initilize(maxHealth);
    }

    public override void OnDamageTaken()
    {
        // Debug.Log(entityName + " taken damage!");

        soundSource.PlayOneShot(HitSFX);
        healthBar.UpdateHealthBar(currentHealth);
    }

    public override void OnHeal()
    {
        // Debug.Log(entityName + " was healed!");
        healthBar.UpdateHealthBar(currentHealth);
    }

    public override IEnumerator DieSequence()
    {
        DisableObjects();
        playerController.DisableMovement();
        soundSource.PlayOneShot(HitSFX);
        soundSource.PlayOneShot(DeathSFX);

        playerAnimator.PlayDeathAnimation();
        playerAnimator.SetWeaponIdleAnimation(false);
        playerAnimator.ChangeGunConstraintWeight(0);
        // Debug.Log(entityName + " is dying...");

        yield return new WaitForSeconds(5.0f);

        // Debug.Log(entityName + " is dead.");

        OnPlayerDeath();
    }

    void OnPlayerDeath()
    {
        // Debug.Log("Gameover");

        GameManager.instance.OnGameEnd();
    }

    void DisableObjects()
    {
        foreach(GameObject obj in ObjectsToDisableOnDeath)
        {
            obj.SetActive(false);
        }
    }
}
