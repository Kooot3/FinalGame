using System;
using DefaultNamespace;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameController : MonoBehaviour

{
    public Player player;
    public TMP_Text coinstext;
    
    private int coins;

    public void Awake()
    {
        player = GetComponent<Player>();
    }
    public void Update()
    {
        player.Move();
        player.Jump();
    }
    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        //Добавляем +1 очко к счету,тем самым конвертируем переменную в строку
        if (coll.gameObject.tag == "Coin")
        {
            coins++;
            coinstext.text = coins.ToString();
            Destroy(coll.gameObject);
        }
    }
}

