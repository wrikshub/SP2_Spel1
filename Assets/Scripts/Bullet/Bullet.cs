using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private BulletHandler bh;
    private Entity shotBy = null;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float speed = 1f;
    
    protected Bullet(Entity whoShotMe)
    {
        shotBy = whoShotMe;
    }

    private void Awake()
    {
        bh = GetComponentInParent<BulletHandler>();
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() == null) return;
        if (!other.GetComponent<Entity>().hostile && !this.hostile) return;
        
        bh.DestroyBullet(0);
        
        other.GetComponent<Health>().TakeDamage(damage, shotBy);
    }
}
