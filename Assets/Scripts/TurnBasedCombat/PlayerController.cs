using UnityEngine;

namespace TurnBasedCombat
{
    public class PlayerController : CombatActor
    {
        [Header("Player Stats")]
        [SerializeField] private int attackPowerDefault = 10;
        [SerializeField] private int attackPowerLimit = 40;
        [SerializeField] private int defensePowerDefault = 0;
        [SerializeField] private int defensePowerLimit = 30;

        private bool hasActed = false;
        private int attackPower;
        private int defensePower;
        public bool HasActed => hasActed;
        public int AttackPower { get => attackPower; set => attackPower = value; }
        public int AttackPowerLimit { get => attackPowerLimit; set => attackPowerLimit = value; }
        public int DefensePower { get => defensePower; set => defensePower = value; }
        public int DefensePowerLimit { get => defensePowerLimit; set => defensePowerLimit = value; }
        public int AttackPowerDefault { get => attackPowerDefault; set => attackPowerDefault = value; }
        public int DefensePowerDefault { get => defensePowerDefault; set => defensePowerDefault = value; }

        protected override void Awake()
        {
            base.Awake();
            attackPower = AttackPowerDefault;
            defensePower = DefensePowerDefault;
        }
        public void ResetTurn()
        {
            hasActed = false;
        }

        public void PerformAttack(EnemyController enemy)
        {
            if (!IsAlive()) return;

            Debug.Log($"Player attacks with power: {AttackPower}");
            enemy.ReceiveDamage(AttackPower);
            AttackPower = AttackPowerDefault;
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
        
        protected override void Die()
        {
            Debug.Log("Player died.");
        }
    }
}