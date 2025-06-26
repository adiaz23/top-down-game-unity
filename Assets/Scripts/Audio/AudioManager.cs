using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip mainWorldMusic;
    
    [Header("SFX Clips")]
    [SerializeField] private AudioClip buttonClickSFX;
    
    [Header("Volume Settings")]
    [SerializeField, Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;
    
    // Dictionary to store all SFX clips
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    
    // Properties to access volumes
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeAudio()
    {
        // Create audio sources if not assigned
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicVolume;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume;
        }
        
        // Initialize SFX dictionary
        if (buttonClickSFX != null) sfxClips.Add("ButtonClick", buttonClickSFX);
    }
    
    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null)
            PlayMusic(mainMenuMusic);
    }
    
    public void PlayMainWorldMusic()
    {
        if (mainWorldMusic != null)
            PlayMusic(mainWorldMusic);
    }
    
    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;
            
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlaySFX(string sfxName)
    {
        if (sfxClips.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
        else
        {
            Debug.LogWarning($"SFX clip {sfxName} not found in the dictionary.");
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip, sfxVolume);
    }
    
    // Volume Control Methods
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
        
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
        
    }
    
    private void Start()
    {
        PlayMainMenuMusic(); 
    }
    

}
