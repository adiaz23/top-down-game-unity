using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/SceneFlowManager")]
public class SceneFlowManager : ScriptableObject
{
    public void LoadMainLevel()
    {
        SceneManager.LoadSceneAsync("MainLevelScene");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadOptionMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
    
     public void LoadConfigMenu()
    {
        SceneManager.LoadSceneAsync("ConfigMenu");
    }
    
    public void LoadWinLevel()
    {
        SceneManager.LoadSceneAsync("WinLevel");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LoadLoseLevel()
    {
        SceneManager.LoadSceneAsync("LoseLevel");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    
    public void LoadLevelMap(string levelName) => SceneManager.LoadSceneAsync(levelName);
}
