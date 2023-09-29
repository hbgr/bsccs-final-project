using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioPlayer")]
public class AudioPlayer : ScriptableObject
{
    [SerializeField]
    private DetachedAudioSource audioObject;

    [SerializeField]
    private int maxAudioSources;

    private Queue<DetachedAudioSource> audioSourcePool = new Queue<DetachedAudioSource>();

    private DetachedAudioSource backgroundMusicSource;    

    private void OnEnable()
    {
        if (audioSourcePool == null)
        {
            audioSourcePool = new Queue<DetachedAudioSource>();
        }
    }

    private void OnDisable()
    {
        audioSourcePool.Clear();
    }

    public void Play(GameObject source, ScriptableAudio audio)
    {
        if (audio.IsBackgroundMusic)
        {
            PlayMusic(source, audio);
        }
        else
        {
            PlayAudio(source, audio);
        }
    }    

    private void PlayAudio(GameObject obj, ScriptableAudio audio)
    {
        DetachedAudioSource audioSource = null;
        if (audioSourcePool.Count >= maxAudioSources)
        {
            audioSource = audioSourcePool.Dequeue();
        }
        if (audioSource == null)
        {
            audioSource = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        }
        audioSource.PlayAudio(obj, audio);
        audioSourcePool.Enqueue(audioSource);
    }

    private void PlayMusic(GameObject obj, ScriptableAudio audio)
    {
        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        }
        backgroundMusicSource.PlayAudio(obj, audio);
    }
}
