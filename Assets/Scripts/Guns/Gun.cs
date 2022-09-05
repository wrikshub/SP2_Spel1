using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEventArgs : EventArgs
{
    public float KnockBack = 0;
}

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private bool usePlayerInput = true;

    public OnGunFired GunFired;

    public delegate void OnGunFired(object sender, GunEventArgs e);


    //Add burst firing mode
    public enum FireMode
    {
        Auto,
        Semi,
        Safe,
        None
    }

    //[Header("Bullet")] [SerializeField] private BulletData bullet;

    public GameObject test;

    //[Header("References")] [SerializeField]
    //private Transform shootPosition;

    [Header("Firemode")] [SerializeField] private FireMode[] AllowedFiringModes =
        {FireMode.Auto, FireMode.Semi, FireMode.Safe};

    protected FireMode currentFireMode;

    [Header("Ammo")] [SerializeField] private int totalAmmo = 25 * 3;
    [SerializeField] private int magSize = 25;
    [SerializeField] private bool infiniteAmmo = false;

    [Header("Spread")] [SerializeField] private float spread = 0.1f;
    [Header("Knockback")] [SerializeField] private float knockback = 1f;

    protected int leftInMag;

    //bah add this later
    //private bool bulletInChamber = false;

    [SerializeField] private float _fireRate;

    protected Gun()
    {
        leftInMag = magSize;
        currentFireMode = AllowedFiringModes[0];
    }

    float FireRate
    {
        get { return _fireRate; }
        set
        {
            if (value > 0)
            {
                _fireRate = value;
            }
        }
    }

    // ReSharper disable once InconsistentNaming
    private float timeSinceLastFired = 0;

    private void Update()
    {
        timeSinceLastFired += Time.deltaTime;
    }

    public void PlayerInput(string fireButton)
    {
        if (!usePlayerInput) return;

        if (Input.GetKeyDown(KeyCode.R) && !infiniteAmmo)
        {
            Reload();
        }

        switch (currentFireMode)
        {
            case FireMode.Auto:
                if (Input.GetButtonDown(fireButton))
                {
                    if (!AttemptFireWeapon())
                    {
                        print("cannot fire weapon!");
                        return;
                    }
                }

                if (Input.GetButton(fireButton) && timeSinceLastFired >= FireRate)
                {
                    FireWeapon();
                }

                break;
            case FireMode.Semi:
                if (Input.GetButtonDown(fireButton) && timeSinceLastFired >= FireRate)
                {
                    if (!AttemptFireWeapon())
                    {
                        print("cannot fire weapon!");
                        return;
                    }

                    FireWeapon();
                }

                break;
            case FireMode.Safe:
                print("its safe !!");
                break;
            case FireMode.None:
                Debug.LogError(
                    $"Firingmode is set to none in class {GetType().Name} in gameobject '{gameObject.name}'!");
                break;
        }
    }
    
    protected void Reload()
    {
        //Add a timer
        ResetAmmo();
    }

    private void ResetAmmo()
    {
        totalAmmo = Mathf.Max(0, totalAmmo -= magSize);
        leftInMag = magSize;
    }

    private bool AttemptFireWeapon()
    {
        if (leftInMag > 0 || infiniteAmmo)
        {
            return true;
        }

        return false;
    }

    protected void FireWeapon()
    {
        Shoot();
        if(GunFired != null)
            GunFired.Invoke(this, new GunEventArgs() {KnockBack = knockback});
        
        timeSinceLastFired = 0;
        if (!infiniteAmmo)
            leftInMag--;
    }

    protected void Shoot()
    {
        //Fix this
        Instantiate(test, transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles +
                             new Vector3(0, 0, UnityEngine.Random.Range(-1f, 1f)).normalized * spread));
    }

    public void SetFireMode(FireMode fm)
    {
        if (!((IList) AllowedFiringModes).Contains(fm))
        {
            Debug.LogError($"'{this}' does not allow firingMode '{fm}'!");
            currentFireMode = FireMode.None;
            return;
        }

        currentFireMode = fm;
    }
}