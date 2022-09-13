using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rbod;
    private Health health;
    private Transform target;
    private Enemy enemy;
    private Entity entity;

    [SerializeField] private float enemySpeed = 10;
    [SerializeField] private float speedlimit = 5;
    [SerializeField] private float hurtPlayerRadius = 1f;
    [SerializeField] private LayerMask hurtLayer;
    private float timeSpentLooking = 0;

    private void Awake()
    {
        rbod = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        enemy = GetComponent<Enemy>();
        entity = GetComponent<Entity>();
    }

    private void Start()
    {
        health.OnTakeDamage += EnemyDamaged;
    }

    private void Update()
    {
        timeSpentLooking += Time.deltaTime;

        CheckforPlayer();

        Navigate();
    }

    private void CheckforPlayer()
    {
        var collider = Physics2D.OverlapCircle(transform.position, hurtPlayerRadius, hurtLayer);

        if (collider != null)
        {
            var health = collider.GetComponent<Health>();
            collider.GetComponent<Entity>()
                .ApplyKnockbackVel((collider.transform.position - transform.position).normalized, enemy.knockbackOnTouch);
            
            health.TakeDamage(new DamageArgs()
            {
                amount = enemy.damageOnTouch, damagedByWho = entity, knockback = enemy.knockbackOnTouch,
                pos = transform.position
            });
        }
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= EnemyDamaged;
    }

    private void Navigate()
    {
        if (target)
        {
            GoToThisPlace(target.position);
        }
        else
        {
            GoToThisPlace(Vector2.zero);
        }
    }

    private void GoToThisPlace(Vector2 coords)
    {
        var dir = ((Vector3) coords - transform.position).normalized;
        rbod.AddForce(dir * enemySpeed * Time.deltaTime, ForceMode2D.Impulse);

        float velocity = rbod.velocity.magnitude;
        float diff = velocity - speedlimit;
        if (velocity >= speedlimit)
        {
            rbod.AddForce(-dir * (diff * Time.deltaTime), ForceMode2D.Impulse);
        }
        else if (-velocity <= -speedlimit)
        {
            rbod.AddForce(dir * (diff * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    private void EnemyDamaged(object s, DamageArgs args)
    {
        SetTarget(args);
    }

    private void SetTarget(DamageArgs args)
    {
        timeSpentLooking = 0;
        target = args.damagedByWho.transform;
    }
}