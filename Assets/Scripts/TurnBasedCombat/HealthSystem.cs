using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TurnBasedCombat
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("Vida")] [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        [SerializeField] private bool isPlayer;

        [Header("Visual Effects")] [SerializeField]
        private DamageFlashEffect damageFlashEffect;

        [SerializeField] private DeathEffect deathEffect;
        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private Transform floatingTextPoint;

        [Header("Sound Effects")] [SerializeField]
        private AudioClip getDamageSound;

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
            ShowFloatingText(amount,false);
            if (damageFlashEffect != null)
            {
                damageFlashEffect.PlayDamageEffect();
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(getDamageSound);
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
            ShowFloatingText(amount,true);
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

        private void ShowFloatingText(int amount, bool isHeal)
        {
            if (floatingTextPrefab != null && floatingTextPoint != null)
            {
                GameObject instance = Instantiate(floatingTextPrefab, floatingTextPoint.position,
                    Quaternion.identity);
                TextMeshPro floatingText = instance.GetComponent<TextMeshPro>();
                floatingText.color = isHeal ? Color.green : (isPlayer ? Color.red : Color.white);
                floatingText.text = (isHeal ? "+" : (isPlayer ? "-" : "")) + amount.ToString();
                Destroy(instance, 1.1f);
            }
        }
    }
}