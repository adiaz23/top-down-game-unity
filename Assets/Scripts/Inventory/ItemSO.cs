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
        GameObject player = GameObject.Find("Player");
        switch (statToChange)
        {
            case Definitions.ItemEffectType.Heal:
                HealthSystem playerHealth = player.GetComponent<HealthSystem>();
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
                PlayerController playerStat = player.GetComponent<PlayerController>();
                if (playerStat.AttackPower >= playerStat.AttackPowerLimit)
                    return false;
                else
                {
                    playerStat.AttackPower += amountToChangeStat;
                }
                return true;
        }
        return false;
    }
}
