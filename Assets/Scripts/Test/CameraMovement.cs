using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    // The target the camera will follow
    public Transform target;
    // The speed with which the camera will follow
    public float smoothing = 5f;

    // The initial offset from the target
    [SerializeField] private Vector3 offset;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - target.position;
    }

    void Move()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
