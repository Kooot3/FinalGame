using System;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
public class PlayerCollisionHandler : MonoBehaviour
{
    public Animator animator;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    private bool isJumping;

    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }
}

