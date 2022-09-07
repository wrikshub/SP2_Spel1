using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private int score = 100;
    
    private void Awake()
    {
        hostile = true;
    }

    internal override void OnDeath(object sender, DamageArgs args)
    {
        base.OnDeath(sender, args);
        
        ScoreManager.Instance.AddScore(score);
    }
}
