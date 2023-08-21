using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


namespace DefaultNamespace
{
    public class Player : MonoBehaviour

   {
       [SerializeField] public float moveSpeed = 5f;
       [SerializeField] public float jumpForce = 5f;
   
       
       private Rigidbody2D rb;
       private float horizontalMove = 0f;
       private bool isFacingRight = true;
       private static bool isJumping = false;
       private bool isGrounded = true;
   
       public Animator animator;
       
       private void Start()
       {
           rb = GetComponent<Rigidbody2D>();
       }
       

       public void Move()
       {
           horizontalMove = Input.GetAxisRaw("Horizontal");
           animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
           rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
           Flip();

           if (Input.GetButtonDown("Jump") )
           {
               Jump();
           }
       }

       //
       public void Jump()
       {
           if (Input.GetButtonDown("Jump")&& isGrounded && !isJumping)
           {
               rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
               isJumping = true;
               animator.SetBool("IsJumping", true);
               //Debug.Log("Jumping: " + isJumping);
           }
       }
       //Разворачиваем персонажа по горизонтали
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
       //Проверяем столкновение с землей и если так,то выключаем прыжок и анимацию
       private void OnCollisionEnter2D(Collision2D collision)
       {
           if (collision.gameObject.CompareTag("Ground"))
           {
               isJumping = false;
               animator.SetBool("IsJumping", false);
               //Debug.Log("Ground" + isJumping);
           }

          
       }
       
   }
}