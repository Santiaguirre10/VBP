using System;
using UnityEngine.Audio;    
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "NewSound", menuName = "GameSettings/Sounds/Create new Sound", order = 1), ]
public class Sound : ScriptableObject
{
    [SerializeField]private string name;
    [SerializeField]private AudioClip clip;
    [Range(0f, 1f)]
    [SerializeField]private float volume;
    [Range(0f, 3f)]
    [SerializeField]private float pitch;
    [HideInInspector] private AudioSource source;
    [SerializeField]private bool loop;
    
    public string Name => name;
    public AudioClip Clip => clip;
    public float Volume => volume;
    public float Pitch => pitch;
    public AudioSource Source
    {
        get => source;
        set => source = value;
    }
    public bool Loop
    {
        get => loop;
        set => loop = value;
    }
}

