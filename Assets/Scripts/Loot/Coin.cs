using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Coin : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem PickupEffect;
    public AudioSource SoundSource;
    public AudioClip PickupSFX;
    public GameObject Model;

    // Private Variables
    bool hasBeenTriggered;

    void OnEnable()
    {
        hasBeenTriggered = false;
        PickupEffect.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        Model.SetActive(true);
    }

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
        ObjectPool.instance.ReturnObject(gameObject, ObjectPool.ObjectTypes.coin, 0.8f);
    }
}
