using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects/MenuManager")]
public class ApplicationManager : ScriptableObject
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private SceneFlowManager sceneManager;
}
