using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinScript : MonoBehaviour
{
    void Start()
    {
        CoinUI.tempCoin = 0;

        if (SceneManager.GetActiveScene().name == "Main_menu")
        {
            CoinUI.totalScore = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CoinUI.tempCoin++;
        Debug.Log(CoinUI.totalScore.ToString());
        Debug.Log(CoinUI.tempCoin.ToString());
        Debug.Log("Hello");
        Destroy(gameObject);
    }
}
