using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    public float CameraSize { get; private set; }

    private void Start()
    {
        CameraSize = vcam.m_Lens.OrthographicSize;
    }

    public void SetCameraZoom(float camZoomAmount)
    {
        vcam.m_Lens.OrthographicSize = camZoomAmount;
    }
}
