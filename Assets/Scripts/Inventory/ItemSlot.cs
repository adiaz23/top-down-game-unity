using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public bool IsFull { get => isFull; set => isFull = value; }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        IsFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;
    }
}
