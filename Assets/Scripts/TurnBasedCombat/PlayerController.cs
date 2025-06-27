using UnityEngine;

namespace TurnBasedCombat
{
    public class PlayerController : CombatActor
    {
        [Header("Player Stats")]
        [SerializeField] private int attackPower = 10;
        [SerializeField] private int attackPowerLimit = 40;

        private bool hasActed = false;
        public bool HasActed => hasActed;
        public int AttackPower { get => attackPower; set => attackPower = value; }
        public int AttackPowerLimit { get => attackPowerLimit; set => attackPowerLimit = value; }

        public void ResetTurn()
        {
            hasActed = false;
        }

        public void PerformAttack(EnemyController enemy)
        {
            if (!IsAlive()) return;

            Debug.Log($"Player attacks with power: {AttackPower}");
            enemy.ReceiveDamage(AttackPower);
            hasActed = true;
        }

        public bool TryEscape()
        {
            if (!IsAlive()) return false;

            float chance = Random.Range(0f, 1f);
            bool success = chance < 0.5f;

            if (success)
            {
                Debug.Log("Player escaped!");
            }
            else
            {
                Debug.Log("Player tried to escape but failed!");
            }

            hasActed = true;
            return success;
        }
        /* TODO: use Item
        public void UseItem(ItemData item)
        {
            if (!IsAlive()) return;

            Debug.Log($"Player uses {item.itemName}");

            switch (item.effectType)
            {
                case ItemEffectType.Heal:
                    healthSystem.Heal(item.amount);
                    break;

                case ItemEffectType.BoostAttack:
                    attackPower += item.amount;
                    break;
            }
            hasActed = true;
        }
        */

        protected override void Die()
        {
            Debug.Log("Player died.");
        }
    }
}