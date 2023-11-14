using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform CameraTransform;

    [Header("Settings")]
    public float followSpeed = 1.0f;

    // Private Variables
    Vector3 offset;

    void Start()
    {
        offset = CalculateOffset(CameraTransform);
    }

    void Update()
    {
        CameraTransform.DOMove(transform.position + offset, followSpeed);
    }

    Vector3 CalculateOffset(Transform obj)
    {
        return obj.position - transform.position;
    }
}
