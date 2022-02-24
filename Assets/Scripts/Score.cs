using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Singleton
    private static Score scoreInstance;

    public static Score Instance { get { return scoreInstance; } }

    // Variables
    private Text scoreText;
    private int score = 0;

    
    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    /// <summary>
    /// Adds the given score to the current one
    /// </summary>
    /// <param name="_addedScore"></param>
    public void UpdateScoreAmount(int _addedScore)
    {
        score += _addedScore;
        scoreText.text = score.ToString();
    }
}
