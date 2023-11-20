using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bean : Entity
{ 
    public override void OnDamageTaken()
    {
        Debug.Log(entityName + " taken damage!");
        transform.DOScaleY(0.2f, 0.2f)
            .OnComplete(() =>
            {
                transform.DOScaleY(1, 0.15f);
            });
    }

    public override void OnHeal()
    {
        Debug.Log( entityName + " was healed!");
    }

    public override IEnumerator DieSequence()
    {
        Debug.Log(entityName + " is dying...");

        yield return new WaitForSeconds(0.2f);

        Debug.Log(entityName + " is dead.");
        Destroy(gameObject);
    }
}
