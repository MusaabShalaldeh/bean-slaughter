using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBean : Entity
{ 
    public override void OnDamageTaken()
    {
        Debug.Log(entityName + " taken damage in air!");
    }

    public override void OnHeal()
    {
        Debug.Log(entityName + " was healed!");
    }

    public override IEnumerator DieSequence()
    {
        Debug.Log(entityName + " is dying...");

        yield return new WaitForSeconds(0.2f);

        Debug.Log(entityName + " fell off and is dead.");
        Destroy(gameObject);
    }
}
