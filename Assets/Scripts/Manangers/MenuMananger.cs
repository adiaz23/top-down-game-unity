using TMPro;
using UnityEngine;

namespace Manangers
{
    public class MenuMananger : MonoBehaviour
    {
        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private Transform floatingTextPoint;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public static MenuMananger instance;
        private void Start()
        {
            if (instance == null) instance = this;
        }
        
        public void ShowFloatingText(string message)
        {
            if (floatingTextPrefab != null && floatingTextPoint != null)
            {
                GameObject instance = Instantiate(floatingTextPrefab, floatingTextPoint.position,
                    Quaternion.identity);
                TextMeshPro floatingText = instance.GetComponent<TextMeshPro>();
                floatingText.color = Color.white;
                floatingText.text = message;
                Destroy(instance, 0.9f);
            }
        }

    }
}
