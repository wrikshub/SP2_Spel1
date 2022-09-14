using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : Health
{
    private Entity entity;
    [SerializeField] private AudioEvent alertOnHurt;

    private void Start()
    {
        entity = GetComponent<Entity>();
        invincibleTimer = invincibleTimerMax;
    }

    public override void TakeDamage(DamageArgs d)
    {
        if (!Invincible)
        {
            alertOnHurt.Play();
            GameManager.Instance.HurtEffect();
        }
        
        
        entity.ApplyKnockback((d.pos - (Vector2)transform.position).normalized, d.knockback);
        base.TakeDamage(d);
        MakeInvincible(invincibleTimerMax);
    }
    
    public override void Kill(DamageArgs d)
    {
        base.Kill(d);
        d.damagedByWho.nameHolder.SetActive(true);
    }
}
