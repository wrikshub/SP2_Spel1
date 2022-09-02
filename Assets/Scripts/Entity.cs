using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
    [SerializeField] private string entityName = "Bob Odenkirk";
    [SerializeField] public bool hostile = true;

    [SerializeField] protected Rigidbody2D rbod;

    public void ApplyKnockback(Vector2 dir)
    {
        rbod.AddForce(dir, ForceMode2D.Impulse);
    }
}
