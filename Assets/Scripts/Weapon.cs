using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private float spread = 0.1f;
    private float timeSinceFired = 0f;

    public event FireWeapon OnFire;

    public delegate void FireWeapon(object sender, WeaponArgs args);

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
        bullet.transform.rotation =
            Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-spread, spread));
        Bullet bulletRef = bullet.transform.GetComponentInChildren<Bullet>();
        bulletRef.InitBullet(GetComponentInParent<Entity>());
        var effect = Instantiate(firedEffect, transform.position, transform.rotation, bullet.transform);
        Destroy(effect, 2f);
        impulse.GenerateImpulse(screenshakeAmount);
        OnFire?.Invoke(this, new WeaponArgs(){knockbackAmount = knockback, bullet = bulletRef});
    }
}
