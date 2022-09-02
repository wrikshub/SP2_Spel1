using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    
    //OnTakeDamage?
    public void TakeDamage(float amount, Entity whoDealtIt)
    {
        //Add knockback
        health = Mathf.Max(health -= amount, 0);
    }

    private void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            //Add kill manager here
        }
    }
}
