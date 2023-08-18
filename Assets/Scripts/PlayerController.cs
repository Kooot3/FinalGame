using System;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour

{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 5f;

    private bool isJumping = false;
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool isFacingRight = true;
    public Animator animator;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        Flip();
        rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
            animator.SetBool("IsJumping", true);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMove < 0f || !isFacingRight && horizontalMove > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
