using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Player"))
        {
            int leftOverItems = InventoryManager.Instance.AddItem(itemName, quantity, sprite);
            if (leftOverItems <= 0)
                Destroy(gameObject);
            else
                quantity = leftOverItems;          
        }
    }
}
