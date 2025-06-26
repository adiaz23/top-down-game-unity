using System.Collections;
using UnityEngine;

namespace TurnBasedCombat
{
    public class DamageFlashEffect : MonoBehaviour
    {
        [Header("Flash Settings")]
        [SerializeField] private Color flashColor = Color.red;
        [SerializeField] private float flashDuration = 0.2f;
        [SerializeField] private int flashCount = 3;
        [SerializeField] private AnimationCurve flashCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        
        private SpriteRenderer spriteRenderer;
        private Color originalColor;
        private Coroutine flashCoroutine;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
        }
        
        public void PlayDamageEffect()
        {
            if (spriteRenderer == null) return;
            
            StopAllEffects();
            
            flashCoroutine = StartCoroutine(FlashEffect());
        }
        
        private IEnumerator FlashEffect()
        {
            float flashInterval = flashDuration / flashCount;
            
            for (int i = 0; i < flashCount; i++)
            {
                // Flash to damage color
                float elapsed = 0f;
                while (elapsed < flashInterval / 2)
                {
                    elapsed += Time.deltaTime;
                    float t = flashCurve.Evaluate(elapsed / (flashInterval / 2));
                    spriteRenderer.color = Color.Lerp(originalColor, flashColor, t);
                    yield return null;
                }
                
                // Flash back to original
                elapsed = 0f;
                while (elapsed < flashInterval / 2)
                {
                    elapsed += Time.deltaTime;
                    float t = flashCurve.Evaluate(elapsed / (flashInterval / 2));
                    spriteRenderer.color = Color.Lerp(flashColor, originalColor, t);
                    yield return null;
                }
            }
            
            spriteRenderer.color = originalColor;
            flashCoroutine = null;
        }
        
        public void StopAllEffects()
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
                flashCoroutine = null;
            }
            
            if (spriteRenderer != null)
                spriteRenderer.color = originalColor;
        }
        
        private void OnDestroy()
        {
            StopAllEffects();
        }
    }
}