using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DetachedAudioSource : MonoBehaviour
{
    private GameObject sourceObj;

    [SerializeField]
    private AudioSource audioSource;

    public void PlayAudio(GameObject obj, ScriptableAudio audio)
    {
        sourceObj = obj;

        audioSource.clip = audio.AudioClip;
        audioSource.volume = audio.Volume;
        audioSource.spatialBlend = audio.SpatialBlend;
        audioSource.loop = audio.Loop;

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (sourceObj != null)
        {
            transform.position = sourceObj.transform.position;
        }
    }
}
