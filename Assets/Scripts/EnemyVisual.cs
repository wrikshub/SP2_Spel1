using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Health health;
    private Animator animator;
    private Rigidbody2D rbod;
    private float dirVel;
    [Range(0,1)][SerializeField] private float dirDamp = 0.5f;
    [SerializeField] private Transform visualDir;
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

    private void Update()
    {
        RotatePlayerVisual();
    }
    
    private void RotatePlayerVisual()
    {
        var vel = rbod.velocity.normalized;
        var velAngle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

        float dirAmount = 0f;
        dirAmount = Mathf.SmoothDampAngle(visualDir.transform.eulerAngles.z, velAngle, ref dirVel, dirDamp);
        
        visualDir.rotation = Quaternion.Euler(0, 0, dirAmount);
    }

    private void EnemyDamaged(object s, DamageArgs args)
    {
        animator.SetTrigger(hitParam);
    }
}
