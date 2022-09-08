using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArgs : EventArgs
{
    public Entity damagedByWho;
    public int amount;
}

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private bool dead = false;

    private DamageArgs lastDamageArgs = null;
    public event NoHealth OnNoHealth;
    public event EntityTakeDamage OnTakeDamage;

    public delegate void EntityTakeDamage(object sender, DamageArgs damageArgs);
    public delegate void NoHealth(object sender, DamageArgs damageArgs);
    
    //OnTakeDamage?
    public void TakeDamage(DamageArgs d)
    {
        if (dead) return;

        OnTakeDamage?.Invoke(this, d);
        lastDamageArgs = d;
        
        //Add knockback
        health = Mathf.Max(health -= d.amount, 0);
    }

    private void Update()
    {
        if (dead) return;
        
        if (health == 0)
        {
            OnNoHealth?.Invoke(this, lastDamageArgs);
            dead = true;
        }
    }
    
}
