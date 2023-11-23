using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
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
        Debug.Log(entityName + " is dying...");

        yield return new WaitForSeconds(0.2f);

        Debug.Log(entityName + " is dead.");
        Destroy(gameObject);

        Debug.Log("Gameover");
    }
}
