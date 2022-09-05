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
    public bool FreezeMovement { private set; get; }

    public void ApplyKnockback(Vector2 dir, float amount)
    {
        rbod.AddForce(dir * amount, ForceMode2D.Impulse);
    }

    private void Update()
    {
        //health.
    }

    public void FreezeEntity(bool freeze)
    {
        FreezeMovement = freeze;
    }
}