using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
    private string entityName = "Bob Odenkirk";
    public GameObject nameHolder = null;
    public TextMeshProUGUI nameText = null;
    [SerializeField] private NameList nList;
    [SerializeField] public bool hostile = true;
    [SerializeField] private GameObject hurtEffect = null;
    [SerializeField] private GameObject deathEffect = null;

    [SerializeField] protected Rigidbody2D rbod;
    [SerializeField] protected Health health;
    [SerializeField] private AudioEvent deathSound;
    [SerializeField] private AudioEvent damageSound;

    public bool FreezeMovement { private set; get; }

    private void Start()
    {
        if (nList != null)
        {
            entityName = nList.PickRandomName();
            nameText.text = entityName;
        }
        health.OnNoHealth += OnDeath;
        health.OnTakeDamage += OnHurt;
    }

    private void OnDestroy()
    {
        health.OnNoHealth -= OnDeath;
        health.OnTakeDamage -= OnHurt;
    }

    public void ApplyKnockback(Vector2 dir, float amount)
    {
        if(!health.Invincible)
            rbod.AddForce(dir * amount, ForceMode2D.Impulse);
    }
    
    public void ApplyKnockbackVel(Vector2 dir, float amount)
    {
        if(!health.Invincible)
            rbod.velocity = (dir * amount);
    }
    
    public void FreezeEntity(bool freeze)
    {
        FreezeMovement = freeze;
    }

    private void OnHurt(object sender, DamageArgs args)
    {
        if (hurtEffect == null) return;
        
        var fx = Instantiate(hurtEffect, transform.position, Quaternion.identity);
        Destroy(fx, 3f);
        damageSound.Play(transform, transform.position);
    }

    internal virtual void OnDeath(object sender, DamageArgs args)
    {
        FreezeMovement = true;
        deathSound.Play(null, transform.position);
        //print(this.entityName + " was killed by " + args.damagedByWho.entityName);

        var a =Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(a, 5);
        
        deathSound.Play(transform, transform.position);
        Destroy(gameObject);
    }
}