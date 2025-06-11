using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();
        [Header("Character Info")]
        public string characterName;
        public Sprite characterImage;
        
    }
    
    [System.Serializable]
    public class DialogueEntry
    {
        [TextArea(3, 10)] // minimo de lineas 3 y maximo 10
        public string text;
        public string speakerName;
        public Sprite speakerImage;
    
        [Header("Dialogue Flow")]
        public bool isLastEntry = false;
        public List<DialogueChoice> choices = new List<DialogueChoice>();
    }
    
    [System.Serializable]
    public class DialogueChoice
    {
        public string choiceText;
        public int nextDialogueIndex = -1; // -1 means end dialogue
    }
}