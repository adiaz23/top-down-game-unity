using Definitions;
using TurnBasedCombat;
using UnityEngine;
using UnityEngine.InputSystem;
using Button = UnityEngine.UI.Button;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    [SerializeField] private GameObject InventoryMenu;
    [SerializeField] private ItemSlot[] itemSlots;

    [SerializeField] private ItemSO[] itemSO;
    [SerializeField] private InputAction inventoryAction;
    [SerializeField] private Button closeButton;
    private bool menuActivated;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        inventoryAction.Enable();
        inventoryAction.performed += PerfomOpenInventory;
        closeButton.onClick.AddListener(OpenInventory);
    }

    public void OpenInventory()
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);
    }
    
    void PerfomOpenInventory(InputAction.CallbackContext context)
    {
        bool performAction = true;
        if (BattleManager.instance != null)
        {
            performAction = BattleManager.instance.GetCurrentState() == BattleState.OutOfBattle;
        }
        if (!performAction) return;
        this.OpenInventory();
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

    public bool UseItem(string itemName)
    {
        foreach (var item in itemSO)
        {
            if (item.itemName == itemName)
            {
                bool usable = item.ApplyItem();
                if (usable && BattleManager.instance != null &&  BattleManager.instance.GetCurrentState() != BattleState.OutOfBattle)
                {
                    OpenInventory();
                    BattleManager.instance.OnPlayerAction_UseItem();
                }
                return usable;
            }
        }

        return false;
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