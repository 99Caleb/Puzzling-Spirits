using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void Start()
    {
        CoinUI.tempCoin = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CoinUI.tempCoin++;
        Debug.Log(CoinUI.totalScore);
        Destroy(gameObject);
    }
}
