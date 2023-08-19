using TMPro;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    private float coins = 0;
    [SerializeField] private TMP_Text coinstext;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Добавляем +1 очко к счету,также конвертируем переменную в строку
        if(coll.gameObject.tag == "Coin")
        {
            coins++;
            coinstext.text = coins.ToString();
            Destroy(coll.gameObject);
        }
    }
}
