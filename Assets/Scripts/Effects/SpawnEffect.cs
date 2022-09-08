using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRend;
    private bool hostile = false;

    [SerializeField] private Color evilColor = Color.red;
    [SerializeField] private Color kindColor = Color.green;
    
    private void Update()
    {
        sRend.color = hostile ? evilColor : kindColor;
    }

    public void Init(bool isEvil)
    {
        hostile = isEvil;
    }
}
