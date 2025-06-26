using UnityEngine;

namespace TurnBasedCombat
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("Vida")]
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        
        [Header("Visual Effects")]
        [SerializeField] private DamageFlashEffect damageFlashEffect;
        [SerializeField] private DeathEffect deathEffect;

        private bool IsDead { get; set; }
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        private void Awake()
        {
            ResetHealth();
            if (damageFlashEffect == null)
                damageFlashEffect = GetComponent<DamageFlashEffect>();
            
            if (deathEffect == null)
                deathEffect = GetComponent<DeathEffect>();
        }

        public void TakeDamage(int amount)
        {
            if (IsDead) return;

            currentHealth -= amount;
            currentHealth = Mathf.Max(currentHealth, 0);
            
            if (damageFlashEffect != null)
            {
                damageFlashEffect.PlayDamageEffect();
            }

            if (currentHealth <= 0)
            {
                IsDead = true;
                if (deathEffect != null)
                {
                    deathEffect.PlayDeathEffect();
                }
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
            if (damageFlashEffect != null)
                damageFlashEffect.StopAllEffects();
            
            if (deathEffect != null)
                deathEffect.ResetEffect();
        }
    }
}