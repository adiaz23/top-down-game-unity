using UnityEngine;

namespace TurnBasedCombat
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("Vida")] [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        private bool IsDead { get; set; }
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        private void Awake()
        {
            ResetHealth();
        }

        public void TakeDamage(int amount)
        {
            if (IsDead) return;

            currentHealth -= amount;
            currentHealth = Mathf.Max(currentHealth, 0);

            if (currentHealth <= 0)
            {
                IsDead = true;
            }
        }

        public void Heal(int amount)
        {
            if (IsDead) return;

            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }

        public bool IsAlive()
        {
            return !IsDead;
        }

        private void ResetHealth()
        {
            currentHealth = maxHealth;
            IsDead = false;
        }
    }
}