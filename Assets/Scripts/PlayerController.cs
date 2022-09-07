using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private Vector2 pInput = Vector2.zero;
    public float MouseAngle { private set; get; }
    public float PSpeedRatio { get; private set; }
    private Camera camera = null;
    [SerializeField] private float speedlimit = 10f;
    [SerializeField] private float moveSpeed = 10f;
    
    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        FreezeEntity(false);
    }

    private void Update()
    {
        if (FreezeMovement == true) return;
        
        MovePlayer();
        Aim();
        
        PSpeedRatio = (rbod.velocity.magnitude) / (speedlimit * speedlimit);
        PSpeedRatio = Mathf.Clamp01(PSpeedRatio);
    }
    
    private void Aim()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativeMousePos = mousePos - transform.position;
        MouseAngle = Mathf.Atan2(relativeMousePos.y, relativeMousePos.x) * Mathf.Rad2Deg;
    }

    private void MovePlayer()
    {
        pInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rbod.AddForce(pInput * (moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
        
        //Limit player speed
        float velocity = rbod.velocity.magnitude;
        float diff = velocity - speedlimit;
        if (velocity >= speedlimit)
        {
            rbod.AddForce(-pInput * (diff * Time.deltaTime), ForceMode2D.Impulse);
        }
        else if (-velocity <= -speedlimit)
        {
            rbod.AddForce(pInput * (diff * Time.deltaTime), ForceMode2D.Impulse);
        }
    }
}