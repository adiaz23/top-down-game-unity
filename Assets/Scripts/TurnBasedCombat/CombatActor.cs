using UnityEngine;

namespace TurnBasedCombat
{
    public abstract class CombatActor : MonoBehaviour
    {

        protected HealthSystem healthSystem;

        protected virtual void Awake()
        {
            healthSystem = GetComponent<HealthSystem>();
        }

        public virtual void ReceiveDamage(int amount)
        {
            healthSystem.TakeDamage(amount);

            if (!healthSystem.IsAlive())
            {
                Die();
            }
        }

        public bool IsAlive()
        {
            return healthSystem.IsAlive();
        }

        protected abstract void Die();
    }
}
