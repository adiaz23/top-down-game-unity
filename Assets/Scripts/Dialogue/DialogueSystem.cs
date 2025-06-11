using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [Header("UI References")] public GameObject dialoguePanel;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI speakerNameText;
        public Image speakerImage; // En el scriptable object esta como sprite TODO: OJO
        public Button continueButton; // Este es el de skipear TODO: OJO lo quite de la escena
        public Transform choicesParent;
        public GameObject choiceButtonPrefab; // Este es el de opciones

        [Header("Typewriter Effect")] 
        public float typingSpeed = 0.05f;
        public bool useTypewriterEffect = true;

        private DialogueData currentDialogue;
        private Character currentCharacter; 
        private int currentEntryIndex = 0;
        private bool isTyping = false;
        private Coroutine typingCoroutine;
        private List<Button> choiceButtons = new List<Button>();

        public static DialogueSystem Instance { get; private set; }

        //Eventos
        public System.Action OnDialogueStart;
        public System.Action OnDialogueEnd;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            dialoguePanel.SetActive(false);
            continueButton.onClick.AddListener(OnContinueClicked);
        }

        public void StartDialogue(DialogueData dialogue, Character character)
        {
            if (dialogue == null || dialogue.dialogueEntries.Count == 0) return;

            currentDialogue = dialogue;
            currentCharacter = character; // Store character reference
            currentEntryIndex = 0;
            dialoguePanel.SetActive(true);

            OnDialogueStart?.Invoke();
            DisplayCurrentEntry();
        }


        private void DisplayCurrentEntry()
        {
            if (currentDialogue == null || currentEntryIndex >= currentDialogue.dialogueEntries.Count)
            {
                EndDialogue();
                return;
            }

            DialogueEntry entry = currentDialogue.dialogueEntries[currentEntryIndex];
    
            
            string displayName = GetSpeakerName(entry);
            Sprite displayIcon = GetSpeakerIcon(entry);
        
            speakerNameText.text = displayName;
            if (displayIcon != null)
                speakerImage.sprite = displayIcon;

            // Clear previous choices
            ClearChoices();

            // Display text
            if (useTypewriterEffect)
            {
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                typingCoroutine = StartCoroutine(TypeText(entry.text));
            }
            else
            {
                dialogueText.text = entry.text;
                ShowContinueOptions(entry);
            }
        }
        private string GetSpeakerName(DialogueEntry entry)
        {
            // Priority 1: Entry-specific speaker name
            if (!string.IsNullOrEmpty(entry.speakerName))
                return entry.speakerName;
            
            // Priority 2: DialogueData character name
            if (!string.IsNullOrEmpty(currentDialogue.characterName))
                return currentDialogue.characterName;
            
            // Priority 3: Character class name (FALLBACK)
            if (currentCharacter != null && !string.IsNullOrEmpty(currentCharacter.characterName))
                return currentCharacter.characterName;
            
            // Default
            return "Unknown";
        }
        
        private Sprite GetSpeakerIcon(DialogueEntry entry)
        {
            // Priority 1: Entry-specific speaker icon
            if (entry.speakerImage != null)
                return entry.speakerImage;
            
            // Priority 2: DialogueData character icon
            if (currentDialogue.characterImage != null)
                return currentDialogue.characterImage;
            
            // Priority 3: Character class sprite (FALLBACK)
            if (currentCharacter != null && currentCharacter.characterSprite != null)
                return currentCharacter.characterSprite;
            
            // No icon available
            return null;
        }

        private IEnumerator TypeText(string text)
        {
            isTyping = true;
            dialogueText.text = "";
            continueButton.gameObject.SetActive(false);

            foreach (char letter in text)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;
            ShowContinueOptions(currentDialogue.dialogueEntries[currentEntryIndex]);
        }

        private void ShowContinueOptions(DialogueEntry entry)
        {
            if (entry.choices.Count > 0)
            {
                // Show choices
                CreateChoiceButtons(entry.choices);
            }
            else
            {
                // Show continue button
                continueButton.gameObject.SetActive(true);
            }
        }

        private void CreateChoiceButtons(List<DialogueChoice> choices)
        {
            foreach (DialogueChoice choice in choices)
            {
                GameObject choiceObj = Instantiate(choiceButtonPrefab, choicesParent);
                Button choiceButton = choiceObj.GetComponent<Button>();
                TextMeshProUGUI choiceText = choiceObj.GetComponentInChildren<TextMeshProUGUI>();

                choiceText.text = choice.choiceText;

                int nextIndex = choice.nextDialogueIndex;
                choiceButton.onClick.AddListener(() => OnChoiceSelected(nextIndex));

                choiceButtons.Add(choiceButton);
            }
        }


        private void OnChoiceSelected(int nextIndex)
        {
            ClearChoices();

            if (nextIndex == -1)
            {
                EndDialogue();
                return;
            }

            currentEntryIndex = nextIndex;
            DisplayCurrentEntry();
        }

        private void OnContinueClicked()
        {
            if (isTyping)
            {
                // Skip typing animation
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentDialogue.dialogueEntries[currentEntryIndex].text;
                isTyping = false;
                ShowContinueOptions(currentDialogue.dialogueEntries[currentEntryIndex]);
                return;
            }

            DialogueEntry currentEntry = currentDialogue.dialogueEntries[currentEntryIndex];

            if (currentEntry.isLastEntry)
            {
                EndDialogue();
                return;
            }

            currentEntryIndex++;
            DisplayCurrentEntry();
        }

        private void ClearChoices()
        {
            foreach (Button button in choiceButtons)
            {
                if (button != null) Destroy(button.gameObject);
            }

            choiceButtons.Clear();
        }

        private void EndDialogue()
        {
            ClearChoices();
            dialoguePanel.SetActive(false);
            currentDialogue = null;
            currentCharacter = null;
            currentEntryIndex = 0;

            OnDialogueEnd?.Invoke();
        }

        private void Update()
        {
            // Allow Enter/Space to continue dialogue
            if (dialoguePanel.activeInHierarchy &&
                (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
            {
                if (continueButton.gameObject.activeInHierarchy)
                {
                    OnContinueClicked();
                }
            }
        }
    }
}