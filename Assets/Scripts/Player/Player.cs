using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [Header("References")]
    public PlayerAnimator playerAnimator;
    public PlayerController playerController;

    public override void OnDamageTaken()
    {
        Debug.Log(entityName + " taken damage!");

        soundSource.PlayOneShot(HitSFX);
    }

    public override void OnHeal()
    {
        Debug.Log(entityName + " was healed!");
    }

    public override IEnumerator DieSequence()
    {
        playerController.DisableMovement();
        soundSource.PlayOneShot(HitSFX);
        soundSource.PlayOneShot(DeathSFX);

        playerAnimator.PlayDeathAnimation();
        Debug.Log(entityName + " is dying...");

        yield return new WaitForSeconds(5.0f);

        Debug.Log(entityName + " is dead.");

        OnPlayerDeath();
    }

    void OnPlayerDeath()
    {
        Debug.Log("Gameover");

        SceneManager.LoadScene(0);
    }
}
