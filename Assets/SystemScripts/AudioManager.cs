using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum Sound
{
    // PLAYER_JUMP,
    // PLAYER_HURT,
    // PLAYER_DEATH,

    MENU_MUSIC,
    GAME_MUSIC
    // GAMEOVER_MUSIC
}

public enum AudioChannel
{
    //PLAYER_GENERIC,
    //PLAYER_HURT,
    //PLAYER_DEATH,

    MUSIC
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        // If another instance already exists destroy this one
        if (Instance != null && Instance != this)
        {
            if (masterSlider != null)
            {
                Instance.masterSlider = masterSlider;
            }
            if (musicSlider != null)
            {
                Instance.musicSlider = musicSlider;
            }
            if (SFXSlider != null)
            {
                Instance.SFXSlider = SFXSlider;
            }

            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    List<AudioSource> channels;
    List<AudioClip> sounds = new List<AudioClip>();

    float masterVolume = 1.0f;
    float musicVolume = 0.5f;
    float SFXVolume = 0.5f;

    float ModifiedMusicVolume()
    {
        return musicVolume * masterVolume;
    }

    float ModifiedSFXVolume()
    {
        return SFXVolume * masterVolume;
    }

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    public void Update()
    {
        ReadSliderValues();
    }

    public void Initialize()
    {
        channels = GetComponents<AudioSource>().ToList();

        // // SFX
        // sounds.Add(Resources.Load<AudioClip>("Sounds/Jump_SFX"));
        // sounds.Add(Resources.Load<AudioClip>("Sounds/Hurt_SFX"));
        // sounds.Add(Resources.Load<AudioClip>("Sounds/Death_SFX"));

        // Music
        sounds.Add(Resources.Load<AudioClip>("Audio/Music/Menu Music"));
        sounds.Add(Resources.Load<AudioClip>("Audio/Music/Game Music"));
    }

    public void PlaySound(Sound soundType, AudioChannel channel, bool isMusic = false)
    {
        channels[(int)channel].clip = sounds[(int)soundType];

        if (isMusic)
        {
            channels[(int)channel].volume = ModifiedMusicVolume();
            channels[(int)channel].loop = true;
        }
        else
        {
            channels[(int)channel].volume = ModifiedSFXVolume();
        }

        channels[(int)channel].Play();
    }

    void ReadSliderValues()
    {
        if (masterSlider != null)
        {
            masterVolume = masterSlider.value;
        }

        if (musicSlider != null)
        {
            musicVolume = musicSlider.value;

            if (channels[0] && channels[0].isPlaying)
            {
                channels[0].volume = ModifiedMusicVolume();
            }
        }

        if (SFXSlider != null)
        {
            SFXSlider.value = ModifiedSFXVolume();
        }
    }

    public void PlayMusic(int musicIndex)
    {
        channels[0].Stop();

        channels[0].clip = sounds[musicIndex];
        channels[0].loop = true;

        channels[0].Play();
    }
}