using UnityEngine;
using UnityEngine.SceneManagement;
namespace TurnBasedCombat
{
    public class EnemyController : CombatActor
    {
        [SerializeField] private int attackPower = 10;
        [SerializeField] [Range(0f, 1f)] private float escapeChance = 0.2f;
        private bool hasActed = false;
        public void ResetTurn() => hasActed = false;
        public bool HasActed => hasActed;

        public void DecideAction(PlayerController player)
        {
            if (WantsToEscape())
            {
                bool escaped = TryEscape();
                if (!escaped)
                {
                    Debug.Log("Enemy tried to escape but failed.");
                }
            }
            else
            {
                PerformAttack(player);
            }

            hasActed = true;
        }

        private void PerformAttack(PlayerController player)
        {
            if (player.DefensePower > 0)
            {
                attackPower -= player.DefensePower;
                player.ReceiveDamage(attackPower);
                attackPower += player.DefensePower;
                player.DefensePower = player.DefensePowerDefault;
            }
            else
            {
                player.ReceiveDamage(attackPower);
            }
        }

        private bool WantsToEscape()
        {
            return Random.value < escapeChance;
        }

        private bool TryEscape()
        {
            float escapeSuccessChance = 0.5f;
            bool escaped = Random.value < escapeSuccessChance;

            if (escaped)
            {
                Debug.Log("Enemy escaped!");
            }

            return escaped;
        }

        protected override void Die()
        {
            Debug.Log("Enemy died.");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if(SceneManager.GetActiveScene().name.Equals("LevelOneScene") || SceneManager.GetActiveScene().name.Equals("LevelTwoScene"))
                    SceneManager.LoadScene("BattleScene");
                BattleManager.instance.StartBattle(this);
                
            }
        }
    }
}