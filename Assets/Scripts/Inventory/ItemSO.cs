using Definitions;
using TurnBasedCombat;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;

    public Definitions.ItemEffectType statToChange = new();

    public int amountToChangeStat;


    public bool ApplyItem()
    {
        switch (statToChange)
        {
            case Definitions.ItemEffectType.Heal:
                HealthSystem playerHealth = GameObject.Find("Player").GetComponent<HealthSystem>();
                if (playerHealth.CurrentHealth == playerHealth.MaxHealth)
                    return false;
                else
                {
                    playerHealth.Heal(amountToChangeStat);
                    BattleManager.instance.UpdateHealthBars();
                    return true;
                }
            case Definitions.ItemEffectType.BoostDefence:
                Debug.Log("TODO: BoostDefence");
                return true;
            case Definitions.ItemEffectType.BoostAttack:
                Debug.Log("TODO: BoostAttack");
                return true;
        }
        return false;
    }
}
