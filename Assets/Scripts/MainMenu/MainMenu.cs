using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backFromOptionsButton;
    [SerializeField] private Button backFromCreditsButton;
    
    [Header("Settings")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    [Header("Game Settings")]
    [SerializeField] private string gameSceneName = "Level1";

    private void Start()
    {
        ShowMainMenu();
        
        // Initialize volume sliders
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = AudioManager.Instance?.MusicVolume ?? 0.5f;
        
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = AudioManager.Instance?.SFXVolume ?? 0.5f;
        
        
        // Add button listeners
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
        
        if (optionsButton != null)
            optionsButton.onClick.AddListener(ShowOptions);
        
        if (creditsButton != null)
            creditsButton.onClick.AddListener(ShowCredits);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
        
        if (backFromOptionsButton != null)
            backFromOptionsButton.onClick.AddListener(ShowMainMenu);
        
        if (backFromCreditsButton != null)
            backFromCreditsButton.onClick.AddListener(ShowMainMenu);
        
        // Add slider listeners
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void ShowMainMenu()
    {
        if (mainPanel != null) mainPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }
    private void ShowCredits()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }
    private void ShowOptions()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    private void StartGame()
    {
        switch (gameSceneName)
        {
            case "MainLevelScene":
                AudioManager.Instance.PlayMainWorldMusic();
                break;
            
        }

    SceneManager.LoadScene(gameSceneName);
    }
    private void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private void SetMusicVolume(float volume)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(volume);
    }
    
    private void SetSFXVolume(float volume)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(volume);
    }
}
