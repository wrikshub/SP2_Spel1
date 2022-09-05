using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject gunHolder = null;
    [SerializeField] private Entity entity = null;
    private Gun gun;
    [SerializeField] private Effects effects;

    private void OnDisable()
    {
        gun.GunFired -= OnFireKnockback;
    }

    private void Start()
    {
        gun = gunHolder.transform.GetComponentInChildren<Gun>();
        gun.GunFired += OnFireKnockback;
    }

    private void Update()
    {
        if (entity.FreezeMovement) return;
        //effects.SpawnEffect(Vector3.zero, transform.localRotation.eulerAngles.z, 1);
        gun.PlayerInput("Fire1");
        
    }

    private void OnFireKnockback(object sender, GunEventArgs g)
    {
        entity.ApplyKnockback(-gunHolder.transform.right, g.KnockBack);
    }
}