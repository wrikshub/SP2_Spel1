using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    public float CameraSize { get; private set; }

    private void Awake()
    {
        CameraSize = vcam.m_Lens.OrthographicSize;
    }

    public void SetCameraZoom(float camZoomAmount)
    {
        vcam.m_Lens.OrthographicSize = camZoomAmount;
    }
}
