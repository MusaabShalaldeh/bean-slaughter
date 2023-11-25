using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    public TrailRenderer trailRenderer;

    Vector3 direction;
    float speed;
    float impactDamage;
    string targetTag;
    bool isActive;
    bool hasBeenReset = false;
    int bulletType;
    
    public void Shoot(Vector3 _direction, float _speed, float _impactDamage, string _targetTag, int _bulletType)
    {
        trailRenderer.enabled = true;
        hasBeenReset = false;
        bulletType = _bulletType;
        direction = _direction;
        speed = _speed;
        targetTag = _targetTag;
        impactDamage = _impactDamage;
        isActive = true;
        
        StartCoroutine(ResetBullet(5));
    }

    void OnTargetImpact(Entity e)
    {
        e.TakeDamage(impactDamage);
        StartCoroutine(ResetBullet());
    }

    IEnumerator ResetBullet(float time = 0.0f)
    {
        if (hasBeenReset)
            yield break;

        yield return new WaitForSeconds(time);

        hasBeenReset = true;
        trailRenderer.enabled = false;
        direction = new Vector3(0, 0, 0);
        speed = 0;
        impactDamage = 0;
        targetTag = "";
        isActive = false;

        ObjectPool.instance.ReturnObject(gameObject, (ObjectPool.ObjectTypes)bulletType);
    }

    void Update()
    {
        if (!isActive) return;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive || other.tag != targetTag) return;

        isActive = false;
        OnTargetImpact(other.GetComponent<Entity>());
    }
}
