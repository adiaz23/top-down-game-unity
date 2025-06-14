using Dialogue;
using UnityEngine;

namespace Characters
{
    public class Enemy : Character
    {
        [Header("Enemy Specific")]
        [SerializeField] private DialogueData battleStartDialogue;
        [SerializeField] private DialogueData defeatDialogue;
    
        private bool isDefeated = false;
    
        public void StartBattleDialogue()
        {
            if (battleStartDialogue != null)
            {
                DialogueSystem.Instance?.StartDialogue(battleStartDialogue, this);
            }
        }
    
        public void OnDefeat()
        {
            isDefeated = true;
            if (defeatDialogue != null)
            {
                DialogueSystem.Instance?.StartDialogue(defeatDialogue, this);
            }
        }
    
        public override void StartDialogue()
        {
            if (isDefeated && defeatDialogue != null)
            {
                DialogueSystem.Instance?.StartDialogue(defeatDialogue, this);
            }
            else
            {
                base.StartDialogue();
            }
        }
    }
}