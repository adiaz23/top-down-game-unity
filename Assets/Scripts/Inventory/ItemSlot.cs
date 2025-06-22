using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool isFull;
    [SerializeField] private int maxNumberOfItems;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    [SerializeField] private GameObject selectedShader;

    private InventoryManager inventoryManager;
   
    public bool thisItemSelected;

    public bool IsFull { get => isFull; set => isFull = value; }
    public GameObject SelectedShader { get => selectedShader; set => selectedShader = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public string ItemName { get => itemName; set => itemName = value; }

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();   
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        if (isFull)
            return quantity;

        this.ItemName = itemName;

        this.itemSprite = itemSprite;
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

        this.Quantity += quantity;

        if (this.Quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            IsFull = true;

            int extraItems = this.Quantity - maxNumberOfItems;
            this.Quantity = maxNumberOfItems;
            return extraItems;
        }

        quantityText.text = this.Quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        SelectedShader.SetActive(true);
        thisItemSelected = true;
    }
}
