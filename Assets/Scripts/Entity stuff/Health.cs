using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArgs : EventArgs
{
    public Entity damagedByWho;
    public int amount;
    public float knockback;
    public Vector2 pos;
}

public class Health : MonoBehaviour
{
    public int CurrentHealth = 5;
    public bool Invincible { private set; get; }
    private float invincibleTimerMax = 0f;
    private float invincibleTimer = 0f;
    public bool Dead { private set; get; }

    private Entity myEntity;
    
    private DamageArgs lastDamageArgs = null;
    public event NoHealth OnNoHealth;
    public event EntityTakeDamage OnTakeDamage;
    public event HealthInvincible OnInvincible;
    public event NoLongerInvincible OnNotInvincible;

    public delegate void EntityTakeDamage(object sender, DamageArgs damageArgs);
    public delegate void NoHealth(object sender, DamageArgs damageArgs);
    public delegate void HealthInvincible(object sender, float duration);
    public delegate void NoLongerInvincible(object sender, float duration);

    private void Awake()
    {
        myEntity = GetComponent<Entity>();
    }

    public void TakeDamage(DamageArgs d)
    {
        if (Dead) return;
        
        OnTakeDamage?.Invoke(this, d);
        lastDamageArgs = d;
        
        //Add knockback
        myEntity.ApplyKnockback(((Vector2)transform.position - d.pos).normalized, d.knockback);
        CurrentHealth = Mathf.Max(CurrentHealth -= d.amount, 0);
    }

    private void Update()
    {
        if (Dead) return;

        invincibleTimer += Time.deltaTime;
        if (invincibleTimer >= invincibleTimerMax)
        {
            Invincible = false;
        }
        
        if (CurrentHealth == 0)
        {
            OnNoHealth?.Invoke(this, lastDamageArgs);
            Dead = true;
        }
    }

    public void MakeInvincible(float duration)
    {
        OnInvincible?.Invoke(this, duration);
    }
    
}
