using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bean : Entity
{
    [Header("References")]
    public EnemyController enemyController;
    public EnemyAnimator enemyAnimator;
    public LootSource lootSource;

    public override void OnDamageTaken()
    {
        // Debug.Log(entityName + " taken damage!");
        Model.transform.DOScaleY(0.2f, 0.2f)
            .OnComplete(() =>
            {
                Model.transform.DOScaleY(1, 0.15f);
            });

        soundSource.PlayOneShot(HitSFX);
    }

    public override void OnHeal()
    {
        // Debug.Log( entityName + " was healed!");
    }

    public override IEnumerator DieSequence()
    {
        enemyController.DisableMovement();
        soundSource.PlayOneShot(HitSFX);
        soundSource.PlayOneShot(DeathSFX);

        // Debug.Log(entityName + " is dying...");
        enemyAnimator.PlayDeathAnimation();

        yield return new WaitForSeconds(0.5f);

        // Debug.Log(entityName + " is dead.");
        lootSource.DropRewards();
        RoundsManager.instance.OnEnemyDeath();
        Destroy(gameObject);
    }
}
