using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private Entity shotBy = null;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float speed = 1f;
    
    protected Bullet(Entity whoShotMe)
    {
        shotBy = whoShotMe;
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() == null) return;
        if (!other.GetComponent<Entity>().hostile && !this.hostile) return;

        other.GetComponent<Health>().TakeDamage(damage, shotBy);
    }
}
