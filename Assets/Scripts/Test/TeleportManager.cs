using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/TeleportManager")]
public class TeleportManager : ScriptableObject
{
    [SerializeField] private string lastSceneName;
    [SerializeField] private string currentSceneName;

    public string LastSceneName
    {
        get => lastSceneName;
    }
    
    private void OnEnable()
    {
        lastSceneName = "";
        currentSceneName = "";
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        lastSceneName = currentSceneName;
        currentSceneName = arg0.name;
        Debug.Log($"{currentSceneName} loaded");
    }
}
