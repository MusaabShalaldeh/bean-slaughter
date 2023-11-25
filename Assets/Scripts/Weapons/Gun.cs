using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum BulletType
    {
        blue = 5,
        red = 6,
    }

    [Header("References")]
    public GameObject Barrel;
    public AudioSource SoundSource;

    [Header("Sound")]
    public AudioClip ShootSFX;

    [Header("Graphic Effect")]
    public ParticleSystem ShootGFX;

    [Header("Settings")]
    public BulletType bulletType;
    public float bulletDamage = 10;
    public float bulletSpeed = 0.2f;
    public string targetTag = "Enemy";

    // Private Variables

    public void Shoot(Transform target, Action PlayAnimation)
    {
        GameObject b = ObjectPool.instance.GetObject((ObjectPool.ObjectTypes)bulletType, Barrel.transform.position);

        Vector3 targetPostion = new Vector3(target.position.x, target.position.y + 1.2f, target.position.z);
        Vector3 direction = targetPostion - Barrel.transform.position;

        Projectile projectile = b.GetComponent<Projectile>();

        PlayAnimation();
        PlayShootSound();
        PlayShootEffect();
        projectile.Shoot(direction, bulletSpeed, bulletDamage, targetTag, (int)bulletType);
    }

    void PlayShootSound()
    {
        if (SoundSource == null || ShootSFX == null)
            return;

        SoundSource.PlayOneShot(ShootSFX);
    }

    void PlayShootEffect()
    {
        if (ShootGFX == null) 
            return;

        ShootGFX.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        ShootGFX.Play();
    }
}
