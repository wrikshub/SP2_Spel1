using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletToFire;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private AudioEvent fireSound;
    [SerializeField] private Transform aimAtPlayer = null;


    [SerializeField] private float timeSinceFiredRandMin, timeSinceFiredRandMax;
    private float timeSinceFiredMax = 1;
    private float timeSinceFired = 0;
    private EnemyControl enemyControl = null;
    private Entity entity;
    
    
    private void Start()
    {
        enemyControl = GetComponent<EnemyControl>();
        timeSinceFiredMax = Random.Range(0f, 2f);
        entity = GetComponent<Entity>();
    }

    private void Update()
    {
        if (enemyControl.Target == null) return;
        
        timeSinceFired += Time.deltaTime;

        if (timeSinceFired >= timeSinceFiredMax)
        {
            Fire();
        }
    }

    private void Fire()
    {
        timeSinceFired = 0;
        timeSinceFiredMax = Random.Range(timeSinceFiredRandMin, timeSinceFiredRandMax);
        
        var bullet = Instantiate(bulletToFire, transform.position, aimAtPlayer.transform.rotation);
        bullet.transform.GetComponentInChildren<Bullet>().InitBullet(entity);
        fireSound.Play(null, transform.position);
        var a = Instantiate(fireEffect, transform.position, quaternion.identity);
        Destroy(a, 2f);
    }
}
