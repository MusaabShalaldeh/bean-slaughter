using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public GameObject Barrel;
    public GameObject Bullet;
    public AudioSource SoundSource;

    [Header("Sound")]
    public AudioClip ShootSFX;

    [Header("Graphic Effect")]
    public ParticleSystem ShootGFX;

    [Header("Settings")]
    public float bulletDamage = 10;
    public float bulletSpeed = 0.2f;
    public string targetTag = "Enemy";

    // Private Variables

    public void Shoot(Transform target, Action PlayAnimation)
    {
        GameObject b = Instantiate(Bullet, Barrel.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
        Vector3 targetPostion = new Vector3(target.position.x, target.position.y + 1.2f, target.position.z);
        Vector3 direction = targetPostion - Barrel.transform.position;

        Projectile projectile = b.GetComponent<Projectile>();

        PlayAnimation();
        PlayShootSound();
        PlayShootEffect();
        projectile.Shoot(direction, bulletSpeed, bulletDamage, targetTag);
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
