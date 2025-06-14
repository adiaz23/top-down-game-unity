using Dialogue;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Character Info")]
        public string characterName;
        public Sprite characterSprite;
    
        [Header("Dialogue")]
        public DialogueData[] dialogues;
        public int currentDialogueIndex = 0;
    
        protected virtual void Start()
        {
            
        }
    
        public virtual void StartDialogue()
        {
            if (dialogues.Length == 0) return;
        
            DialogueData dialogue = dialogues[Mathf.Clamp(currentDialogueIndex, 0, dialogues.Length - 1)];
            DialogueSystem.Instance?.StartDialogue(dialogue, this);
        }
    
        public virtual void SetDialogueIndex(int index)
        {
            currentDialogueIndex = Mathf.Clamp(index, 0, dialogues.Length - 1);
        }
    
        public virtual void NextDialogue()
        {
            if (currentDialogueIndex < dialogues.Length - 1)
            {
                currentDialogueIndex++;
            }
        }
    }

}