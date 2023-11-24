using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem PickupEffect;
    public AudioSource SoundSource;
    public AudioClip PickupSFX;
    public GameObject Model;

    // Private Variables
    bool hasBeenTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered || other.tag != "Player")
            return;

        hasBeenTriggered = true;
        OnCoinPickup();
    }

    void OnCoinPickup()
    {
        Model.SetActive(false);
        PickupEffect.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        PickupEffect.Play();
        SoundSource.PlayOneShot(PickupSFX);

        GameManager.instance.EarnCoin(1);
        Destroy(gameObject, 0.8f);
    }
}
