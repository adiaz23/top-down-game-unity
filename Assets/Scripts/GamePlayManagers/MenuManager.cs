using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scriptable Objects/MenuManager")]
public class MenuManager : ScriptableObject
{
    [SerializeField] private List<Button> buttons = new();
    [SerializeField] private SceneFlowManager sceneFlowManager;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) => InitializeButtonList();
    
    private void AssingButtonEvent(Button button)
    {
        switch (button.gameObject.tag)
        {
            case "MainLevelButton":
                button.onClick.AddListener(
                    delegate
                    {
                        sceneFlowManager.LoadMainLevel();
                    });
                break;
            case "ConfigMenuButton":
                button.onClick.AddListener(
                    delegate
                    {
                        sceneFlowManager.LoadConfigMenu();
                    });
                break;
            case "OptionMenuButton":
                button.onClick.AddListener(
                    delegate
                    {
                        sceneFlowManager.LoadOptionMenu();
                    });
                break;
            case "QuitButton":
                button.onClick.AddListener(
                    delegate
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
		                Application.Quit();
#endif
                    });
                break;
        }
    }
    
    private void AssignButtonListEvent()
    {
        foreach (var button in buttons)
        {
            AssingButtonEvent(button);
        }
    }

    private void PopulateButtonList()
    {
        foreach (var button in FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            buttons.Add(button);
        }
    }

    private void InitializeButtonList()
    {
        buttons.Clear();
        PopulateButtonList();
        AssignButtonListEvent();
    }
}
