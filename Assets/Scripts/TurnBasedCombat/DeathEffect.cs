using System.Collections;
using UnityEngine;

namespace TurnBasedCombat
{
    public class DeathEffect : MonoBehaviour
    {
        [Header("Death Animation Settings")]
        [SerializeField] private float deathDuration = 1.5f;
        [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        
        [Header("Rotation Settings")]
        [SerializeField] private bool enableRotation = true;
        [SerializeField] private float rotationSpeed = 360f; 
        [SerializeField] private AnimationCurve rotationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        
        [Header("Sound Effect")]
        [SerializeField] private string deathSFXName = "Death";
        
        private SpriteRenderer spriteRenderer;
        private Vector3 originalScale;
        private Color originalColor;
        private bool isPlayingDeathEffect = false;
        
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalScale = transform.localScale;
            
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
        }
        
        public void PlayDeathEffect()
        {
            if (isPlayingDeathEffect) return;
            
            isPlayingDeathEffect = true;
            
            if (AudioManager.Instance != null && !string.IsNullOrEmpty(deathSFXName))
            {
                AudioManager.Instance.PlaySFX(deathSFXName);
            }
            
            StartCoroutine(DeathAnimationCoroutine());
        }
        
        private IEnumerator DeathAnimationCoroutine()
        {
            float elapsed = 0f;
            
            while (elapsed < deathDuration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = elapsed / deathDuration;
                
                float scaleValue = scaleCurve.Evaluate(normalizedTime);
                transform.localScale = originalScale * scaleValue;
                
                if (spriteRenderer != null)
                {
                    float fadeValue = fadeCurve.Evaluate(normalizedTime);
                    Color newColor = originalColor;
                    newColor.a = fadeValue;
                    spriteRenderer.color = newColor;
                }
                
                if (enableRotation)
                {
                    float rotationValue = rotationCurve.Evaluate(normalizedTime);
                    float currentRotation = rotationValue * rotationSpeed * elapsed;
                    transform.rotation = Quaternion.Euler(0, 0, currentRotation);
                }
                
                yield return null;
            }
            
            transform.localScale = Vector3.zero;
            if (spriteRenderer != null)
            {
                Color finalColor = originalColor;
                finalColor.a = 0f;
                spriteRenderer.color = finalColor;
            }
           
            
            gameObject.SetActive(false);
        }
        
        public void ResetEffect()
        {
            isPlayingDeathEffect = false;
            transform.localScale = originalScale;
            transform.rotation = Quaternion.identity;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }
}