using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    [SerializeField] AudioClip[] clips;

    [Range(0, 1)] [SerializeField] private float volume = 1;
    [SerializeField] private AudioMixer audioMixer;

    [Range(0, 1)] [SerializeField] float spatialBlend = 0;

    [SerializeField] private float minPitch, maxPitch;
    [SerializeField] private bool loop = false;
    [SerializeField] private bool destroyOnFinish = true;
    [SerializeField] private float distanceFalloff = 11f;

    public override GameObject Play()
    {
        if (clips.Length == 0) return null;

        AudioSource source = CreateAudioHolderObject(null, Vector3.zero);

        SetAudioValues(source);

        source.PlayOneShot(source.clip);
        return source.gameObject;
    }

    public override GameObject Play(Transform t, Vector3 offset)
    {
        if (clips.Length == 0) return null;

        AudioSource source = CreateAudioHolderObject(t, offset);

        SetAudioValues(source);

        source.PlayOneShot(source.clip);
        return source.gameObject;
    }
    
    
    private void SetAudioValues(AudioSource source)
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.pitch = Random.Range(minPitch, maxPitch);
        source.volume = volume;
        source.outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;
        source.loop = loop;
        source.spatialBlend = spatialBlend;
        source.minDistance = distanceFalloff;
        
        if (destroyOnFinish)
        {
            Destroy(source.gameObject, source.clip.length + 0.5f);
        }
    }
}