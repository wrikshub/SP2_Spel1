using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletTrail;
    private float timer = 0f;
    private float timerLimit = 5f;
    private bool beginCountdown = false;

    private void Awake()
    {
        DestroyBullet(5f);
    }

    private void Update()
    {
        if(beginCountdown)
            timer += Time.deltaTime;
        
        if(timer > timerLimit - 0.5f)
            bulletTrail.transform.SetParent(transform);

    }

    public void DestroyBullet(float time)
    {
        timerLimit = time;
        Destroy(transform.GetChild(0).gameObject, time);
        Destroy(gameObject, time + 5f);
    }
    
}
