using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Trigger : MonoBehaviour
{
    [Header("Settings")]
    public string targetTag;

    public abstract void OnStartTouch(Collider obj);

    public abstract void OnStay(Collider obj);

    public abstract void OnEndTouch(Collider obj);

    void OnTriggerEnter(Collider other)
    {
        OnStartTouch(other);
    }

    void OnTriggerStay(Collider other)
    {
        OnStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnEndTouch(other);
    }
}
