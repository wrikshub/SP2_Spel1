using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health = 100f;
    
    //OnTakeDamage?
    public void TakeDamage(float amount, Entity whoDealtIt)
    {
        health -= amount;
    }
}
