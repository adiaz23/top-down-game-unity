using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject InventoryMenu;
    [SerializeField] private ItemSlot[] itemSlots;
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

    public int AddItem(string itemName, int quantity, Sprite sprite)
    {
        foreach (var slot in itemSlots)
        {
            if (!slot.IsFull && (slot.ItemName == itemName || slot.Quantity == 0))
            {
                int leftOverItems = slot.AddItem(itemName, quantity, sprite);

                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, sprite);
                }
                
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in itemSlots)
        {
            slot.SelectedShader.SetActive(false);
            slot.thisItemSelected = false;
        }
    }
}
