using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
        invincibleTimer = invincibleTimerMax;
    }

    public override void TakeDamage(DamageArgs d)
    {
        entity.ApplyKnockback((d.pos - (Vector2)transform.position).normalized, d.knockback);
        base.TakeDamage(d);
        MakeInvincible(invincibleTimerMax);
    }
}
