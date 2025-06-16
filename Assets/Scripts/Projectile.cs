using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D collision2D;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collision2D = gameObject.GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        StartCoroutine(SelfDestruct());   
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        collision2D.enabled = false;
    }
}
