using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class AudioEvent : ScriptableObject
{
    public abstract GameObject Play();
    public abstract GameObject Play(Transform t, Vector3 offset);
    public AudioSource CreateAudioHolderObject(Transform t, Vector3 offset)
    {
        GameObject sound = new GameObject();
        var audio = sound.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        sound.transform.parent = t;
        sound.transform.localPosition = offset;
        sound.name = "AudioEvent: " + audio.name;
        
        return audio;
    }
}
