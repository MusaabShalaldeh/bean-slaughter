using System.Collections.Generic;
using UnityEngine;

public class ModelEvents : MonoBehaviour
{
    [Header("References")]
    public AudioSource SoundSource;
    public ParticleSystem FootParticle;

    [Header("Footstep Clips")]
    public List<AudioClip> concreteFootsteps; // default footstep sounds

    public void PlayRandomFootstep()
    {
        AudioClip clip = concreteFootsteps[Random.Range(0, concreteFootsteps.Count)];
        SoundSource.pitch = Random.Range(1f, 1.1f);


        SoundSource.PlayOneShot(clip);
    }

    public void PlayFootParitcle()
    {
        if (FootParticle == null) 
            return;

        FootParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        FootParticle.Play();
    }
}
