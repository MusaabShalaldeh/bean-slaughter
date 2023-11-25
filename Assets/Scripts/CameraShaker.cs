using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    #region singleton
    public static CameraShaker instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("References")]
    public Camera cam;
    bool isShaking = false;

    public void ShakeCamera(float duration, float amount)
    {
        if (isShaking) return;
        isShaking = true;

        Vector3 s = new Vector3(amount, 0, 0);
        cam.DOShakeRotation(duration, strength: s).OnComplete(() =>
        {
            isShaking = false;
        });
    }
}
