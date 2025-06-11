using Dialogue;
using UnityEngine;

namespace Characters
{
    public class NPC : Character
    {
        [Header("NPC Specific")]
        public bool hasQuest = false;
        public bool questCompleted = false;
    
        protected override void Start()
        {
            base.Start();
        
            // Set up dialogue trigger
            DialogueTrigger trigger = GetComponent<DialogueTrigger>();
            if (trigger == null)
            {
                trigger = gameObject.AddComponent<DialogueTrigger>();
            }
        
            // Set initial dialogue
            if (dialogues.Length > 0)
            {
                trigger.dialogueData = dialogues[currentDialogueIndex];
            }
        }
    
        public override void StartDialogue()
        {
            // Update dialogue based on quest status
            UpdateDialogueBasedOnQuestStatus();
            base.StartDialogue();
        }
    
        private void UpdateDialogueBasedOnQuestStatus()
        {
            if (!hasQuest) return;
        
            if (questCompleted && currentDialogueIndex == 0)
            {
                // Switch to completion dialogue if available
                if (dialogues.Length > 1)
                {
                    SetDialogueIndex(1);
                }
            }
        }
    
        public void CompleteQuest()
        {
            questCompleted = true;
            UpdateDialogueBasedOnQuestStatus();
        }
    }
}