using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShakeData
{
    
}

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float totalShakeAmount = 0f;
    private float baselineShake = 0f;
    [SerializeField] private float shakeReduction = 0.1f;
    
    private void Start()
    {
        perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
    }

    private void Update()
    {
        perlin.m_AmplitudeGain = baselineShake + totalShakeAmount;
        totalShakeAmount -= shakeReduction;
    }

    private void Shake(float amount)
    {
        totalShakeAmount += amount;
    }

    public void ChangeBaselineShake(float amount)
    {
        baselineShake = amount;
    }
}
