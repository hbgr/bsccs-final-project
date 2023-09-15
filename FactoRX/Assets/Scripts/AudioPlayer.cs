using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioPlayer")]
public class AudioPlayer : ScriptableObject
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private DetachedAudioSource audioObject;

    [SerializeField]
    private int maxAudioSources;

    private Queue<DetachedAudioSource> audioSourcePool;

    private DetachedAudioSource backgroundMusicSource;

    private void OnEnable()
    {
        events.PlayAudioEvent += OnPlayAudio;
        audioSourcePool = new Queue<DetachedAudioSource>();
    }

    private void OnDisable()
    {
        events.PlayAudioEvent -= OnPlayAudio;
        audioSourcePool.Clear();
    }

    private void OnPlayAudio(object sender, ScriptableAudio audio)
    {
        if (sender is not GameObject)
        {
            return;
        }

        GameObject obj = sender as GameObject;

        if (audio.IsBackgroundMusic)
        {
            PlayMusic(obj, audio);
        }
        else
        {
            PlayAudio(obj, audio);
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
