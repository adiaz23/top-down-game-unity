using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement System")]
    [SerializeField] private InputAction moveAction;
    [SerializeField] private float speed = 4;

    private Rigidbody2D rb;
    private Vector2 lookDirection = new Vector2(1, 0);
    private Vector2 currentInput;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetMovementEnabled(true);
    }

    void Update()
    {
        UpdateMoveInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 position = rb.position;
        position = position + currentInput * speed * Time.deltaTime;
        rb.MovePosition(position);        
    }

    public void SetMovementEnabled(bool isEnabled)
    {
        if (isEnabled)
            moveAction.Enable();
        else
            moveAction.Disable();
    }

    void UpdateMoveInput()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        currentInput = move;
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }
}
