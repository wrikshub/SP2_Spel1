using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string invincibleAnimPropertyBool = "invincible";
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private PlayerController pCont;
    [SerializeField] private Rigidbody2D rbod;
    [SerializeField] private Transform visualDir;
    [SerializeField] private Transform visualAim;
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private float cameraShakeAmount = 2f;
    private CameraControl cCont;
    private Health health;
    private float dirVel;
    private float cameraZoomVel;
    private float lookVel;
    private float camZoomCurrent;
    [Range(0, 1)] [SerializeField] private float dirDamp = 0.5f;
    [Range(0, 1)] [SerializeField] private float cameraZoomDamp = 0.5f;
    [Range(0, 1)] [SerializeField] private float lookDamp = 0.5f;
    [Range(0, 5)] [SerializeField] private float cameraZoomAmount = 2;
    private float originalSize;

    private void Start()
    {
        cCont = Camera.main.GetComponent<CameraControl>();
        originalSize = cCont.CameraSize;
        camZoomCurrent = cCont.CameraSize;
        cameraZoomVel = 0f;
        health = GetComponent<Health>();
        health.OnInvincible += OnPlayerChangeInvincibleState;
        health.OnNotInvincible += OnPlayerChangeInvincibleState;
        health.OnTakeDamage += OnDamaged;
    }

    private void OnDisable()
    {
        health.OnInvincible -= OnPlayerChangeInvincibleState;
        health.OnNotInvincible -= OnPlayerChangeInvincibleState;
        health.OnTakeDamage -= OnDamaged;
    }

    void Update()
    {
        RotatePlayerVisual();
        CameraZoom();
        Aim();
        ParticleSystemMakeInteresting();
    }

    private void ParticleSystemMakeInteresting()
    {
        var main = ps.main;
        main.startSpeed = pCont.PSpeedRatio * 1.125f;
    }

    private void CameraZoom()
    {
        camZoomCurrent = Mathf.SmoothDamp(camZoomCurrent, pCont.PSpeedRatio * cameraZoomAmount, ref cameraZoomVel,
            cameraZoomDamp);
        cCont.SetCameraZoom(Mathf.Clamp(camZoomCurrent + originalSize, 0, 7.5f));
    }

    private void RotatePlayerVisual()
    {
        var vel = rbod.velocity.normalized;
        var velAngle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

        float dirAmount = 0f;
        dirAmount = Mathf.SmoothDampAngle(visualDir.transform.eulerAngles.z, velAngle, ref dirVel, dirDamp);

        visualDir.rotation = Quaternion.Euler(0, 0, dirAmount);
    }

    private void Aim()
    {
        float aimAmount = 0f;
        aimAmount = Mathf.SmoothDampAngle(visualAim.transform.eulerAngles.z, pCont.MouseAngle, ref lookVel, lookDamp);

        visualAim.localRotation = Quaternion.Euler(visualAim.localRotation.eulerAngles.x,
            visualAim.localRotation.eulerAngles.y,
            aimAmount);
    }

    private void OnPlayerChangeInvincibleState(object s, float d, bool isInvis)
    {
        anim.SetBool(invincibleAnimPropertyBool, isInvis);
    }

    private void OnDamaged(object s, DamageArgs damageArgs)
    {
        impulse.m_DefaultVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * cameraShakeAmount;
        impulse.GenerateImpulse();
    }
}