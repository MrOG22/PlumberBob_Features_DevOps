using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scorePerCoin = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinPickUp();
    }

    /// <summary>
    /// Updates score and disables the coin
    /// </summary>
    private void CoinPickUp()
    {
        if (Score.Instance != null)
        {
            Score.Instance.UpdateScoreAmount(scorePerCoin);
        }
        
        gameObject.SetActive(false);
    }
}
