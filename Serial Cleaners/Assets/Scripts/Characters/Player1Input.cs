using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player1Input : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 300;

    // Animation.
    [SerializeField] private Animator myAnimator;

    private bool isMoving;
    private bool isForward;
    private bool isDiagonal;
    private bool isSideways;
    private bool isBackwardDiagonal;
    private bool isBackward;
    private bool mirror;

    PlayerControls controls;
    Vector2 move;
    Vector2 lastMove;
    Vector2 look;

    private void Start()
    {
        // Find components.
        myAnimator = GetComponentInChildren<Animator>();
        look = new Vector2(0, -1);
        lastMove = new Vector2(0, -1);
    }

    private void Update()
    {
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        //Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        //direction = direction.normalized;

        //// Translate the gameobject
        //transform.position += direction * speed * Time.deltaTime;

        //// Rotate the gameobject
        //if (direction != Vector3.zero)
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), rotationSpeed * Time.deltaTime);

        Vector3 direction = new Vector3(move.x, 0.0f, move.y);
        direction = direction.normalized;

        transform.position += direction * speed * Time.deltaTime;

        if(look != Vector2.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(look.x, 0, look.y), Vector3.up), rotationSpeed * Time.deltaTime);

        //// Update animation variables.
        //isMoving = !Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f);

        // Update animator.
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        float dot = Vector2.Dot(lastMove, look);
        isForward = dot >= 0.90;
        isDiagonal = 0.25 <= dot && dot < 0.90;
        isSideways = -0.25 <= dot && dot < 0.25;
        isBackwardDiagonal = -0.75 <= dot && dot < -0.25;
        isBackward = dot < -0.75;
        mirror = lastMove.x * look.y - lastMove.y * look.x < 0;

        myAnimator.SetBool("IsForward", isForward);
        myAnimator.SetBool("IsMoving", isMoving);
        myAnimator.SetBool("IsDiagonal", isDiagonal);
        myAnimator.SetBool("IsSideways", isSideways);
        myAnimator.SetBool("IsBackwardDiagonal", isBackwardDiagonal);
        myAnimator.SetBool("IsBackward", isBackward);
        myAnimator.SetBool("Mirror", mirror);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            move = -context.ReadValue<Vector2>();
            if(move != Vector2.zero)
            {
                lastMove = move.normalized;
                isMoving = true;
            }
        }
        else if (context.canceled)
        {
            move = Vector2.zero;
            isMoving = false;
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            look = -context.ReadValue<Vector2>();
            look = look.normalized;
        }
    }
}
