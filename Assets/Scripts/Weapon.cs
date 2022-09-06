using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponArgs : EventArgs
{
    public float knockbackAmount = 1;
    public Bullet bullet = null;
}

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletToFire;
    [SerializeField] private GameObject firedEffect;
    private CinemachineImpulseSource impulse;
    [SerializeField] private float screenshakeAmount = 0.1f;
    [SerializeField] private float knockback = 10f;
    [SerializeField] private float cooldown = 0.1f;
    private float timeSinceFired = 0f;

    public event Delegate OnFire;

    public delegate void Delegate(object sender, WeaponArgs args);

    private void Awake()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
        timeSinceFired = cooldown;
    }

    private void Update()
    {
        Cooldown();
    }

    private void Cooldown()
    {
        timeSinceFired += Time.deltaTime;
    }

    public void Fire()
    {
        if (timeSinceFired < cooldown) return;
        timeSinceFired = 0;
        GameObject bullet = Instantiate(bulletToFire, transform.position, transform.rotation);
        var effect = Instantiate(firedEffect, transform.position, transform.rotation, bullet.transform);
        impulse.GenerateImpulse(screenshakeAmount);
        OnFire?.Invoke(this, new WeaponArgs(){knockbackAmount = knockback, bullet = bullet.GetComponent<Bullet>()});
    }
}
