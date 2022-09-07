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
    [SerializeField] private GameObject[] guns;
    private int gunSelected = 0;
    
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

        if (Input.GetButtonDown("Fire2"))
        {
            gunSelected++;
            if (gunSelected >= guns.Length)
                gunSelected = 0;
            
            SwitchWeapon(guns[gunSelected]);
        }
    }

    private void OnFireKnockback(object sender, WeaponArgs args)
    {   
        whoHoldsMe.ApplyKnockback(-gunHolder.transform.right, args.knockbackAmount);
    }

    public void SwitchWeapon(GameObject weapon)
    {
        gun.OnFire -= OnFireKnockback;
        Destroy(gunHolder.transform.GetChild(0).gameObject);
        GameObject gunInst = Instantiate(weapon, gunHolder.transform.position, Quaternion.Euler(0,0,0), gunHolder.transform);
        gunInst.transform.localRotation = Quaternion.identity;
        gun = gunInst.GetComponent<Weapon>();
        gun.OnFire += OnFireKnockback;
    }
}