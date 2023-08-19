
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 5f;

    
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool isFacingRight = true;
   
    public Animator animator;
    public  static bool isJumping = false;
   

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
            isJumping = true;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            PlayerController.isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }
}
