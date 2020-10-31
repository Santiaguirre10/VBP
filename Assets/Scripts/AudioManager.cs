using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField,ReadOnly]private Sound[] sounds;
    [SerializeField,ReadOnly]private Sound[] music;
    
    [SerializeField] private GameObject musicButton;
    [SerializeField] private Sprite musicOff;
    [SerializeField] private Sprite musicOn;
    
    [SerializeField] private GameObject soundButton;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite soundOn;
    public Sound[] Sounds
    {
        get => sounds;
        set => sounds = value;
    }

    public Sound[] Music
    {
        get => music;
        set => music = value;
    }

    public bool Mute
    {
        get => mute;
        set => mute = value;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch; 
        }
        foreach (Sound m in music)
        {
            m.Source = gameObject.AddComponent<AudioSource>();
            m.Source.clip = m.Clip;
            m.Source.volume = m.Volume;
            m.Source.pitch = m.Pitch; 
        }
    }

    private void Start()
    {
        PlayMusic("MusicIntro");
    }

    private void Update()
    {
        if (mute == true)
        {
            soundButton.GetComponent<Image>().sprite = soundOff;
            foreach (Sound s in sounds)
            {
                s.Source.volume = 0f;
            }
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOn;
            foreach (Sound s in sounds)
            {
                s.Source.volume = 1f;
            }
        }
        if (muteMusic == true)
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
            foreach (Sound m in music)
            {
                m.Source.volume = 0f;
            }
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
            foreach (Sound m in music)
            {
                m.Source.volume = 1f;
            }
        }
    }

    #region InitilizeAudioManager
    
#if UNITY_EDITOR
    [Button]
    private void LoadAllSounds()
    {
        sounds = Resources.LoadAll<Sound>("GameData/Sounds");
    }
    [Button]
    private void LoadAllMusic()
    {
        music = Resources.LoadAll<Sound>("GameData/Music");
    }
    private void OnValidate()
    {
        InitializeAudioManager();
    }
#endif
    private void InitializeAudioManager()
    {
        if (null == sounds)
        {
            LoadAllSounds();
            LoadAllMusic();
        }
    }
    
    #endregion

    private bool mute;

    public bool MuteMusic1
    {
        get => muteMusic;
        set => muteMusic = value;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.Source.Play();
    }
    public void MuteSounds()
    {
        if (mute == true)
        {
            mute = false;
        }
        else
        {
            mute = true;
        }
    }

    private bool muteMusic;

    public bool Mutemusic
    {
        get => muteMusic;
        set => muteMusic = value;
    }

    public void PlayMusic(string name)
    {
        Sound m = Array.Find(music, Sound => Sound.name == name);
        if (m == null)
        { 
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        m.Source.Play();
    }
    public void MuteMusic()
    {
        if (muteMusic == true)
        {
            muteMusic = false;
        }
        else
        {
            muteMusic = true;
        }
    }
}
