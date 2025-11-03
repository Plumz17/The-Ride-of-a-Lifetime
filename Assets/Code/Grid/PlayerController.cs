using UnityEngine.InputSystem;
using UnityEngine;
using System;
using NUnit.Framework;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask collisionLayer;

    private InputActions inputActions;
    private Vector2 moveInput;
    private bool isMoving = false;

    void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Movement.started += OnButtonPressed;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Movement.started -= OnButtonPressed;
        inputActions.Player.Disable();
    }

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            isMoving = false;
        }
    } 

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (isMoving) return; //This feels unresponsive at times
        moveInput = context.ReadValue<Vector2>();
        moveInput = new Vector2(Mathf.Round(moveInput.x), Mathf.Round(moveInput.y));

        Debug.Log(moveInput);

        Vector3 targetPos = movePoint.position + new Vector3(moveInput.x, moveInput.y, 0);

        if (!Physics2D.OverlapCircle(targetPos, 0.2f, collisionLayer))
        {
            movePoint.position = targetPos;
            isMoving = true;
        }
    }
}
