using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    float impactDamage;
    string targetTag;
    bool isActive;

    public void Shoot(Vector3 _direction, float _speed, float _impactDamage, string _targetTag)
    {
        direction = _direction;
        speed = _speed;
        targetTag = _targetTag;
        impactDamage = _impactDamage;
        isActive = true;
        Debug.Log("bullet fired");
        Destroy(gameObject, 5);
    }

    void OnTargetImpact(Entity e)
    {
        e.TakeDamage(impactDamage);
        Destroy(gameObject);
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
