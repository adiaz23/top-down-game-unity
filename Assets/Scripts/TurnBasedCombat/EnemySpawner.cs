namespace TurnBasedCombat
{
   using System.Collections.Generic;
   using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnPoint
    {
        public GameObject enemyPrefab;
        public Transform spawnPosition;
        public string enemyId; // Can be used for quest tracking
        public bool spawnOnStart = true;
        public Vector3 enemyScale = Vector3.one;
    }

    [Header("Enemy Spawn Points")] [SerializeField]
    private List<EnemySpawnPoint> spawnPoints;
    
    [Header("Spawn Settings")]
    [SerializeField] private bool visualizeSpawnPoints = true;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float gizmoSize = 0.5f;

    // Dictionary to track spawned enemies by ID
    private Dictionary<string, GameObject> spawnedEnemies;

    private void Start()
    {
        // Spawn all enemies marked for auto-spawn
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.spawnOnStart)
            {
                SpawnEnemy(spawnPoint);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!visualizeSpawnPoints) return;

        Gizmos.color = gizmoColor;
        
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.spawnPosition != null)
            {
                Gizmos.DrawSphere(spawnPoint.spawnPosition.position, gizmoSize);
            }
        }
    }

    // Spawn an enemy at a specific spawn point
    private GameObject SpawnEnemy(EnemySpawnPoint spawnPoint)
    {
        if (spawnPoint.enemyPrefab == null || spawnPoint.spawnPosition == null)
        {
            Debug.LogWarning("Cannot spawn enemy: missing prefab or spawn position");
            return null;
        }

        // Check if this enemy is already spawned
        if (!string.IsNullOrEmpty(spawnPoint.enemyId) && 
            spawnedEnemies.ContainsKey(spawnPoint.enemyId) && 
            spawnedEnemies[spawnPoint.enemyId] != null)
        {
            // Enemy already exists
            return spawnedEnemies[spawnPoint.enemyId];
        }

        // Spawn the enemy
        GameObject enemy = Instantiate(
            spawnPoint.enemyPrefab, 
            spawnPoint.spawnPosition.position, 
            Quaternion.identity
        );
        
        enemy.transform.localScale = spawnPoint.enemyScale;

        // Add to tracking dictionary if it has an ID
        if (!string.IsNullOrEmpty(spawnPoint.enemyId))
        {
            spawnedEnemies[spawnPoint.enemyId] = enemy;
        }

        return enemy;
    }


    

  
    
    
}
}