using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject gunHolder = null;
    [SerializeField] private PlayerController pCont = null;
    private Gun gun;
    [SerializeField] private Effects effects;

    private void Start()
    {
        //Resources.Load("Resources/walla");
        gun = gunHolder.transform.GetComponentInChildren<Gun>();
    }

    private void Update()
    {
        if (pCont.freezeMovement) return;
        //effects.SpawnEffect(Vector3.zero, transform.localRotation.eulerAngles.z, 1);
        gun.PlayerInput("Fire1");
    }
}