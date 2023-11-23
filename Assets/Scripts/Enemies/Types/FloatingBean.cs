using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBean : Entity
{
    [Header("References")]
    public EnemyController enemyController;
    public EnemyAnimator enemyAnimator;

    public override void OnDamageTaken()
    {
        Debug.Log(entityName + " taken damage in air!");
        Model.transform.DOScaleY(0.2f, 0.2f)
            .OnComplete(() =>
            {
                Model.transform.DOScaleY(1, 0.15f);
            });

        soundSource.PlayOneShot(HitSFX);
    }

    public override void OnHeal()
    {
        Debug.Log(entityName + " was healed!");
    }

    public override IEnumerator DieSequence()
    {
        enemyController.DisableMovement();
        soundSource.PlayOneShot(HitSFX);
        soundSource.PlayOneShot(DeathSFX);

        Debug.Log(entityName + " is dying...");
        enemyAnimator.PlayDeathAnimation();

        yield return new WaitForSeconds(0.25f);

        Debug.Log(entityName + " fell off and is dead.");
        Destroy(gameObject);
    }
}
