namespace MainMenuAndScene
{
    using UnityEngine;

    public class LevelLoader : MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private string levelName;
        [SerializeField] private Vector2 playerSpawnPosition;
    
        [Header("Player Respawn")]
        [SerializeField] private bool respawnPlayerAtPosition = true;
    
        private void Start()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMainWorldMusic();
            }
            
            if (respawnPlayerAtPosition)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = playerSpawnPosition;
                }
            }
        }
        
        private void OnDrawGizmos()
        { 
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerSpawnPosition, 0.5f);
        }
    }
}