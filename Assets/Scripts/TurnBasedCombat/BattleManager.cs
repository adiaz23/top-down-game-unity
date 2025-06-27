using Definitions;
using UnityEngine;

namespace TurnBasedCombat
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager instance;
        public PlayerController player;
        public Player playerMovement;
        public EnemyController enemy;
        public CombatUIManager uiManager;
        private BattleState currentState;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            currentState = BattleState.OutOfBattle;   
        }

        public BattleState GetCurrentState()
        {
            return currentState;
        }
        public void StartBattle(EnemyController enemyInstance)
        {
            currentState = BattleState.Start;

            enemy = enemyInstance;
            enemy.ResetTurn();
            playerMovement.SetMovementEnabled(false);
            player.ResetTurn();
            UpdateHealthBars();
            uiManager.SetVisibilityCombatUI(true);
            uiManager.ShowMessage("¡Un enemigo salvaje apareció!");
            Invoke(nameof(BeginPlayerTurn), 1.5f);
        }

        private void BeginPlayerTurn()
        {
            currentState = BattleState.PlayerTurn;
            player.ResetTurn();

            uiManager.ShowMessage("Tu turno.");
            uiManager.ShowActionOptions();
        }

        public void OnPlayerAction_Attack()
        {
            if (currentState != BattleState.PlayerTurn) return;

            currentState = BattleState.Busy;
            uiManager.HideActionOptions();

            player.PerformAttack(enemy);
            uiManager.UpdateHealthBars();

            Invoke(nameof(HandleEnemyTurnOrEnd), 1f);
        }

        public void OnPlayerAction_UseItem()
        {
            Invoke(nameof(HandleEnemyTurnOrEnd), 1f);
        }
        public void OnPlayerAction_Escape()
        {
            if (currentState != BattleState.PlayerTurn) return;

            currentState = BattleState.Busy;
            uiManager.HideActionOptions();

            bool escaped = player.TryEscape();

            if (escaped)
            {
                EndBattle(BattleState.Escape);
                playerMovement.SetMovementEnabled(true);
            }
            else
            {
                uiManager.ShowMessage("¡No pudiste escapar!");
                Invoke(nameof(BeginEnemyTurn), 1f);
            }
        }

        private void BeginEnemyTurn()
        {
            if (!enemy.IsAlive())
            {
                EndBattle(BattleState.Victory);
                return;
            }

            currentState = BattleState.EnemyTurn;
            enemy.ResetTurn();

            uiManager.ShowMessage("Turno del enemigo.");

            Invoke(nameof(EnemyAct), 1f);
        }

        private void EnemyAct()
        {
            enemy.DecideAction(player);
            uiManager.UpdateHealthBars();

            if (!player.IsAlive())
            {
                EndBattle(BattleState.Defeat);
                playerMovement.SetMovementEnabled(true);
            }
            else if (!enemy.IsAlive())
            {
                EndBattle(BattleState.Victory);
                playerMovement.SetMovementEnabled(true);
            }
            else
            {
                Invoke(nameof(BeginPlayerTurn), 1f);
            }
        }

        private void HandleEnemyTurnOrEnd()
        {
            if (!enemy.IsAlive())
            {
                EndBattle(BattleState.Victory);
                playerMovement.SetMovementEnabled(true);
            }
            else
            {
                BeginEnemyTurn();
            }
        }

        public void EndBattle(BattleState result)
        {
            currentState = result;

            switch (result)
            {
                case BattleState.Victory:
                    uiManager.ShowMessage("¡Has ganado!");
                    break;
                case BattleState.Defeat:
                    uiManager.ShowMessage("Has sido derrotado...");
                    break;
                case BattleState.Escape:
                    uiManager.ShowMessage("Escapaste con éxito.");
                    break;
            }
            currentState = BattleState.OutOfBattle;
            Invoke(nameof(OcultarUICombat), 1.5f);
        }
        
        private void OcultarUICombat()
        {
            uiManager.SetVisibilityCombatUI(false);
        }

        public void UpdateHealthBars()
        {
            uiManager.UpdateHealthBars();
        }
    }
}