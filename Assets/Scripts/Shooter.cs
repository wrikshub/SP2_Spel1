using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject gunHolder = null;
    [SerializeField] private Entity whoHoldsMe = null;
    private Weapon gun;
    [SerializeField] private string fireButton = "Fire1";
    
    private void Start()
    {
        //make this dynamic
        gun = gunHolder.transform.GetComponentInChildren<Weapon>();
        gun.OnFire += OnFireKnockback;
    }

    private void OnDestroy()
    {
        gun.OnFire -= OnFireKnockback;
    }

    private void Update()
    {
        if (whoHoldsMe.FreezeMovement) return;
        //effects.SpawnEffect(Vector3.zero, transform.localRotation.eulerAngles.z, 1);
        if(Input.GetButton(fireButton))
            gun.Fire();
        
    }

    private void OnFireKnockback(object sender, WeaponArgs args)
    {
        //impulse.GenerateImpulse(-gunHolder.transform.right * knockback);
        //impulse.GenerateImpulseAt(transform.position, -gunHolder.transform.right * g.KnockBack * 100);
        whoHoldsMe.ApplyKnockback(-gunHolder.transform.right, args.knockbackAmount);
    }
}