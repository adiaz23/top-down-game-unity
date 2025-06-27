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
        PlayerController playerStat = player.GetComponent<PlayerController>();
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();

        switch (statToChange)
        {
            case Definitions.ItemEffectType.Heal:
                if (playerHealth.CurrentHealth == playerHealth.MaxHealth)
                    return false;
                else
                {
                    playerHealth.Heal(amountToChangeStat);
                    BattleManager.instance.UpdateHealthBars();
                    return true;
                }
            case Definitions.ItemEffectType.BoostDefence:
                if (playerStat.DefensePower >= playerStat.DefensePowerLimit)
                    return false;
                else
                {
                    playerStat.DefensePower += amountToChangeStat;
                }
                    Debug.Log($"TODO: BoostDefense: {playerStat.DefensePower}");
                return true;
            case Definitions.ItemEffectType.BoostAttack:
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
