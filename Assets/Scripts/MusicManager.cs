using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer am;
    [SerializeField] private AudioMixerSnapshot[] playingSnapshot;
    [SerializeField] private AudioMixerSnapshot[] idleSnapshot;
    [SerializeField] private float snapshotTransitionTime = 2f;
    public static MusicManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameStarted()
    {
        am.TransitionToSnapshots(playingSnapshot, new float[1]{1}, snapshotTransitionTime);
    }

    public void GameOver()
    {
        am.TransitionToSnapshots(idleSnapshot, new float[1]{1}, snapshotTransitionTime);
    }
    
}
