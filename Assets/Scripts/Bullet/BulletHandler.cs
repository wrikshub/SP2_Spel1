using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletTrail;
    private Bullet bullet;
    private float timer = 0f;
    private float timerLimit = 5f;
    private bool beginCountdown = false;

    private void Awake()
    {
        bullet = GetComponentInChildren<Bullet>();
        RemoveBullet(5f);
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
        bulletTrail.transform.parent = transform;
        timerLimit = time;
        SpawnEffect();
        Destroy(transform.GetChild(0).gameObject, time);
        Destroy(gameObject, time + 5f);
    }
    
    private void RemoveBullet(float time)
    {
        Destroy(gameObject, time);
    }

    private void SpawnEffect()
    {
        var bulletEffect = Instantiate(bullet.hitEffect, bullet.transform.position, Quaternion.identity, transform);
        Destroy(bulletEffect, 5f);
    }
}
