using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rbod;
    private Health health;
    public Transform Target;
    [SerializeField] private Transform aimThisAtTarget = null;
    private Enemy enemy;
    private Entity entity;

    [SerializeField] private float enemySpeed = 10;
    [SerializeField] private float speedlimit = 5;
    [SerializeField] private float hurtPlayerRadius = 1f;
    [SerializeField] private LayerMask hurtLayer;
    private float timeSpentNavigatingMax = 1;
    private float timeSpentNavigating = 0;
    
    private Vector2 placeToGoTo = Vector2.zero;

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
        timeSpentNavigating += Time.deltaTime;

        CheckforPlayer();
        Navigate();

        if (Target == null) return;
        if (aimThisAtTarget == null) return;

        var aimDelta = (Target.position - transform.position).normalized;
        var angle = Mathf.Atan2(aimDelta.y, aimDelta.x) * Mathf.Rad2Deg;
        aimThisAtTarget.transform.localRotation = Quaternion.Euler(0, 0, angle);
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
        if (Target)
        {
            GoToThisPlace(Target.position);
        }
        else
        {
            GoToThisPlace(FindRandomPlaceToGoTo(11));
        }
    }

    private Vector2 FindRandomPlaceToGoTo(int bounds)
    {
        if (timeSpentNavigating < timeSpentNavigatingMax) return placeToGoTo;
        
        timeSpentNavigating = 0;
        timeSpentNavigatingMax = Random.Range(1, 5);
        Vector2 place = new Vector2(Random.Range(-bounds, bounds), Random.Range(-bounds, bounds));
        placeToGoTo = place;
        return place;
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
        timeSpentNavigating = 0;
        Target = args.damagedByWho.transform;
    }
}