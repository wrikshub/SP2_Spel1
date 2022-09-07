using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D rbod;
    private Health health;
    private Animator animator;
    [SerializeField] private string hitParam = "hurt";
    
    private void Awake()
    {
        rbod = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        health.OnTakeDamage += EnemyDamaged;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= EnemyDamaged;
    }

    private void EnemyDamaged(object s, DamageArgs args)
    {
        animator.SetTrigger(hitParam);
    }
}
