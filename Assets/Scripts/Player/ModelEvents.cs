using System.Collections.Generic;
using UnityEngine;

public class ModelEvents : MonoBehaviour
{
    [Header("References")]
    public AudioSource soundSource;

    [Header("Footstep Clips")]
    public List<AudioClip> concreteFootsteps; // default footstep sounds

    public void PlayRandomFootstep()
    {
        AudioClip clip = concreteFootsteps[Random.Range(0, concreteFootsteps.Count)];
        soundSource.pitch = Random.Range(1f, 1.1f);

        soundSource.PlayOneShot(clip);
    }
}
