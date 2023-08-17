using System;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;


public class Player : MonoBehaviour

    {
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float jumpForce = 5f;
        
        private bool isJumping = false;
        private Rigidbody2D rb;
        public Animation animator;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float moveX = Input.GetAxis("Horizontal");

            // Для того чтобы двигаться по оси X
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

            // Прыжок
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
                isJumping = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
        }
    }