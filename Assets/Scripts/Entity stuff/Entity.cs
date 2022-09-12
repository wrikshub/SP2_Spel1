using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Entity : MonoBehaviour
{
    [SerializeField] private string entityName = "Bob Odenkirk";
    [SerializeField] public bool hostile = true;

    [SerializeField] protected Rigidbody2D rbod;
    [SerializeField] protected Health health;
    [SerializeField] private AudioEvent deathSound;
    [SerializeField] private AudioEvent damageSound;
    
    public bool FreezeMovement { private set; get; }

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnNoHealth += OnDeath;
    }

    private void OnDestroy()
    {
        health.OnNoHealth -= OnDeath;
    }

    public void ApplyKnockback(Vector2 dir, float amount)
    {
        if(!health.Invincible)
            rbod.AddForce(dir * amount, ForceMode2D.Impulse);
    }

    public void FreezeEntity(bool freeze)
    {
        FreezeMovement = freeze;
    }

    internal virtual void OnDeath(object sender, DamageArgs args)
    {
        FreezeMovement = true;
        deathSound.Play(null, transform.position);
        print(this.entityName + " was killed by " + args.damagedByWho.entityName);
        Destroy(gameObject);
    }
}