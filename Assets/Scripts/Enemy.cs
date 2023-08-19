using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Vector3[] positions;

    private int _currentTarget;
    
    
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[_currentTarget], speed);
        if (transform.position == positions[_currentTarget])
        {
            if (_currentTarget < positions.Length - 1)
            {
                _currentTarget++;
            }
            else
            {
                _currentTarget = 0;
            }
            {
                Flip();
            }
        }
    }

    //Объект уничтожает игрока при столкновении,тем самым перезапускает сцену 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
    //Чтобы задестроить объект при его убийстве
private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
