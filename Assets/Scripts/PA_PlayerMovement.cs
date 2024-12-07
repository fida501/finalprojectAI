using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PA_PlayerMovement : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    public float speed = 8f;
    public float jumpForce = 10f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Horizontal movement
        Vector3 move = new Vector3(horizontal, 0f, 0f);
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Flip the character
        if (horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight)
        {
            Flip();
        }

        // Update animation
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            velocity.y = jumpForce;
        }
    }
}
