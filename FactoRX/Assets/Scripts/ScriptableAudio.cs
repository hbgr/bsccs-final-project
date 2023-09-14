using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio")]
public class ScriptableAudio : ScriptableObject
{
    [SerializeField]
    private AudioClip audioClip;

    public AudioClip AudioClip => audioClip;

    [SerializeField]
    [Range(0, 1)]
    private float volume;

    public float Volume => volume;

    [SerializeField]
    private bool loop;

    public bool Loop => loop;

    [SerializeField]
    [Range(0, 1)]
    private float spatialBlend;

    public float SpatialBlend => spatialBlend;

    [SerializeField]
    private bool isBackgroundMusic;

    public bool IsBackgroundMusic => isBackgroundMusic;
}
