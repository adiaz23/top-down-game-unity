using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject InventoryMenu;
    [SerializeField] private InputAction inventoryAction;
    private bool menuActivated;

    void Awake()
    {
        inventoryAction.Enable();

        inventoryAction.performed += OpenInventory;
    }

    void OpenInventory(InputAction.CallbackContext context)
    {
        if (menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (!menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}
