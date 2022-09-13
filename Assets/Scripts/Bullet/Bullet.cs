using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private BulletHandler bh;
    private Entity shotBy = null;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speed = 1f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] public GameObject hitEffect = null;
    [SerializeField] private AudioEvent hitSound;
    [SerializeField] private float bKnockback = 10f;
    [SerializeField] private int timesPassThroughEnemy = 0;
    private int timesHit = 0;

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
        if (((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            bh.DestroyBullet(0);
            Hit();
        }

        Entity hit = other.GetComponent<Entity>();
        Health health = other.GetComponent<Health>();

        if (hit == null) return;
        if (health == null) return;

        //Hit enemy
        if (hit.hostile && !hostile)
        {
            timesHit++;
            if (health != null)
                health.TakeDamage(new DamageArgs
                    {amount = damage, damagedByWho = shotBy, pos = shotBy.transform.position, knockback = bKnockback});
            
            Hit();
            if (timesHit > timesPassThroughEnemy)
            {
                bh.DestroyBullet(0);
            }
        }

        //Hit player
        if (!hit.hostile && hostile)
        {
            health.TakeDamage(new DamageArgs
                {amount = damage, damagedByWho = shotBy, pos = shotBy.transform.position, knockback = bKnockback});
            bh.DestroyBullet(0);
        }
    }

    public void InitBullet(Entity whoShotMe)
    {
        shotBy = whoShotMe;
    }

    private void Hit()
    {
        hitSound.Play(null, transform.position);
    }
}