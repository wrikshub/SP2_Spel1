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
    [SerializeField] protected float invincibleTimerMax = 0f;
    protected float invincibleTimer = 0f;
    public bool Dead { private set; get; }

    private Entity myEntity;

    private DamageArgs lastDamageArgs = null;
    public event NoHealth OnNoHealth;
    public event EntityTakeDamage OnTakeDamage;
    public event HealthInvincible OnInvincible;
    public event HealthInvincible OnNotInvincible;

    public delegate void EntityTakeDamage(object sender, DamageArgs damageArgs);

    public delegate void NoHealth(object sender, DamageArgs damageArgs);

    public delegate void HealthInvincible(object sender, float duration, bool invincible);

    private void Awake()
    {
        myEntity = GetComponent<Entity>();
    }

    private void Start()
    {
        invincibleTimer = invincibleTimerMax + 1;
        Invincible = false;
    }

    public virtual void TakeDamage(DamageArgs d)
    {
        if (Dead || Invincible) return;

        myEntity.ApplyKnockback(((Vector2) transform.position - d.pos).normalized, d.knockback);

        OnTakeDamage?.Invoke(this, d);
        lastDamageArgs = d;

        CurrentHealth = Mathf.Max(CurrentHealth -= d.amount, 0);

        if (CurrentHealth == 0)
        {
            OnNoHealth?.Invoke(this, lastDamageArgs);
            Dead = true;
            Kill(d);
        }
    }

    public virtual void Kill(DamageArgs d)
    {
        print("killed :(");
    }
    
    private void Update()
    {
        invincibleTimer += Time.deltaTime;
        if (invincibleTimer >= invincibleTimerMax)
        {
            Invincible = false;
            OnNotInvincible?.Invoke(this, 0, false);
        }
    }

    public void MakeInvincible(float duration)
    {
        if (Invincible) return;

        Invincible = true;
        invincibleTimer = 0;
        invincibleTimerMax = duration;
        OnInvincible?.Invoke(this, duration, true);
    }
}