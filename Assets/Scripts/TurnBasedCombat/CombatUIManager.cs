using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TurnBasedCombat
{
    public class CombatUIManager : MonoBehaviour
    {
        [Header("UI Panels")] [SerializeField] private GameObject actionPanel;
        [SerializeField] private GameObject enemyHealthPanel;
        [SerializeField] private GameObject itemPanel;

        [Header("Buttons")] [SerializeField] private Button attackButton;
        [SerializeField] private Button escapeButton;
        [SerializeField] private Button useItemButton;

        [Header("Health Bars")]
        [SerializeField] private Slider playerHealthBar;

        [SerializeField] private Slider enemyHealthBar;

        [Header("Text Fields")] [SerializeField]
        private TMP_Text messageText;

        private void Start()
        {
            SetVisibilityCombatUI(false);
            // Asignar eventos a los botones
            attackButton.onClick.AddListener(() =>
            {
                EventSystem.current.SetSelectedGameObject(null);
                BattleManager.instance.OnPlayerAction_Attack();
            });
            escapeButton.onClick.AddListener(() =>
            {
                EventSystem.current.SetSelectedGameObject(null);
                BattleManager.instance.OnPlayerAction_Escape();
            });

            /* TODO: Items
            useItemButton.onClick.AddListener(() =>
            {
                ItemData mockItem = new ItemData
                {
                    itemName = "Poción",
                    effectType = ItemEffectType.Heal,
                    amount = 25
                };
                BattleManager.Instance.OnPlayerAction_UseItem(mockItem);
            });
            */

            HideActionOptions();
            HideItemPanel();
            ShowMessage("");
        }

        public void ShowActionOptions()
        {
            actionPanel.SetActive(true);
        }

        public void HideActionOptions()
        {
            actionPanel.SetActive(false);
        }
        
        private void HideItemPanel()
        {
            itemPanel.SetActive(false);
        }

        public void ShowMessage(string message)
        {
            messageText.text = message;
        }

        public void UpdateHealthBars()
        {
            if (BattleManager.instance.player != null)
            {
                var health = BattleManager.instance.player.GetComponent<HealthSystem>();
                playerHealthBar.maxValue = health.MaxHealth;
                playerHealthBar.value = health.CurrentHealth;
            }

            if (BattleManager.instance.enemy != null)
            {
                var health = BattleManager.instance.enemy.GetComponent<HealthSystem>();
                enemyHealthBar.maxValue = health.MaxHealth;
                enemyHealthBar.value = health.CurrentHealth;
            }
        }

        public void SetVisibilityCombatUI(bool isVisible)
        {
            ShowMessage("");
            actionPanel.SetActive(isVisible);
            itemPanel.SetActive(isVisible);
            enemyHealthPanel.SetActive(isVisible); 
        }
    }
}