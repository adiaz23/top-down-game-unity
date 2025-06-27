using System.Collections;
using Manangers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private TeleportManager teleportManager;
    [SerializeField] private string teleportName;
    [SerializeField] private float secondsBetweenTeleports;
    private Coroutine _teleportRoutine;
    private bool _isActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (teleportManager.LastSceneName == teleportName)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
            _isActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) return;
        if (!_isActive) return;
        _teleportRoutine = StartCoroutine(TeleportCountdown());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) return;
        _isActive = true;
        if (_teleportRoutine == null) return;
        StopCoroutine(_teleportRoutine);
        _teleportRoutine = null;
    }

    private IEnumerator TeleportCountdown()
    {
        if (MenuMananger.instance)
        {
            for (int i = Mathf.CeilToInt(secondsBetweenTeleports); i > 0; i--)
            {
                MenuMananger.instance.ShowFloatingText("Teletransporte  en: " + i + "s");
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            yield return new WaitForSeconds(secondsBetweenTeleports);
        }

        SceneManager.LoadScene(teleportName);
    }
}