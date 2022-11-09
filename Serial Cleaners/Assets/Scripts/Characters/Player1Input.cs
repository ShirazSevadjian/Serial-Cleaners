using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Player1Input : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 300;

    // Animation.
    [SerializeField] private Animator myAnimator;
    private bool isMoving;

    private void Start()
    {
        // Find components.
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        direction = direction.normalized;

        // Translate the gameobject
        transform.position += direction * speed * Time.deltaTime;

        // Rotate the gameobject
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), rotationSpeed * Time.deltaTime);


        // Update animation variables.
        isMoving = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f);

        // Update animator.
        myAnimator.SetBool("isMoving", isMoving);

    }
}
