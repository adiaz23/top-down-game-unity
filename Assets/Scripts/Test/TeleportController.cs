using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private TeleportManager teleportManager;
    [SerializeField] private string teleportName;
    [SerializeField] private float secondsBetweenTeleports;
    private Coroutine _teleportRoutine;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (teleportManager.LastSceneName == teleportName)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) return;
        _teleportRoutine = StartCoroutine(TeleportCountdown());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) return;
        if (_teleportRoutine == null) return;
        StopCoroutine(_teleportRoutine);
        _teleportRoutine = null;
    }

    private IEnumerator TeleportCountdown()
    {
        yield return new WaitForSeconds(secondsBetweenTeleports);
        SceneManager.LoadScene(teleportName);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
