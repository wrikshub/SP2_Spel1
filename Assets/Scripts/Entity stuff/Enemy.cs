using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private int score = 100;
    [SerializeField] private GameObject scoreEffect = null;
    
    private void Awake()
    {
        hostile = true;
    }

    internal override void OnDeath(object sender, DamageArgs args)
    {
        base.OnDeath(sender, args);

        GameObject g = Instantiate(scoreEffect, transform.position, Quaternion.identity);
        g.GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
        Destroy(g, 5f);
        
        ScoreManager.Instance.AddScore(score);
    }
}
