using Characters;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Dialogue Settings")] public DialogueData dialogueData;
        [SerializeField] private bool triggerOnStart = false;
        [SerializeField] private bool  triggerOnCollision = true;
        [SerializeField] private KeyCode interactionKey = KeyCode.E; //TODO: move this when possible

        [Header("Interaction Settings")] 
        [SerializeField] private GameObject interactionPrompt; // TODO: use this when it gets contact with a dialogue character
        [SerializeField] private float interactionRange = 2f;

        private bool playerInRange = false;
        private GameObject player;

        private void Start()
        {
            if (triggerOnStart && dialogueData != null)
            {
                TriggerDialogue();
            }

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }

        private void Update()
        {
            if (playerInRange && Input.GetKeyDown(interactionKey))
            {
                TriggerDialogue();
            }

            // Check distance if player reference exists
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                bool inRange = distance <= interactionRange;

                if (inRange != playerInRange)
                {
                    playerInRange = inRange;
                    if (interactionPrompt != null)
                        interactionPrompt.SetActive(playerInRange);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);
            if (triggerOnCollision && other.CompareTag("Player"))
            {
                player = other.gameObject;
                playerInRange = true;

                if (interactionPrompt != null)
                    interactionPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                player = null;
                playerInRange = false;

                if (interactionPrompt != null)
                    interactionPrompt.SetActive(false);
            }
        }

        public void TriggerDialogue()
        {
            if (dialogueData != null && DialogueSystem.Instance != null)
            {
                DialogueSystem.Instance.StartDialogue(dialogueData, GetComponent<Character>());
            }
        }
    }
}