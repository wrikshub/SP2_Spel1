using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private PlayerController pCont;
    [SerializeField] private CameraControl cCont;
    [SerializeField] private Rigidbody2D rbod;
    [SerializeField] private Transform visualDir;
    [SerializeField] private Transform visualAim;
    [SerializeField] private CameraShake cs;
    private float dirVel;
    private float cameraZoomVel;
    private float lookVel;
    private float camZoomCurrent = 0f;
    [Range(0,1)][SerializeField] private float dirDamp = 0.5f;
    [Range(0,1)][SerializeField] private float cameraZoomDamp = 0.5f;
    [Range(0,1)][SerializeField] private float lookDamp = 0.5f;
    [Range(0, 5)][SerializeField] private float cameraZoomAmount = 2;


    private void Start()
    {
        cCont = Camera.main.GetComponent<CameraControl>();
        camZoomCurrent = cCont.CameraSize;
    }

    void Update()
    {
        RotatePlayerVisual();
        CameraPreaim();
        Aim();
        ParticleSystemMakeInteresting();
    }

    private void ParticleSystemMakeInteresting()
    {
        var main = ps.main;
        main.startSpeed = pCont.PSpeedRatio * 1.125f;
    }

    private void CameraPreaim()
    {
        camZoomCurrent = Mathf.SmoothDamp(camZoomCurrent, pCont.PSpeedRatio * cameraZoomAmount, ref cameraZoomVel, cameraZoomDamp);
        cCont.SetCameraZoom(cCont.CameraSize + camZoomCurrent);
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
        
        visualAim.localRotation = Quaternion.Euler(visualAim.localRotation.eulerAngles.x, visualAim.localRotation.eulerAngles.y,
            aimAmount);
    }
}
