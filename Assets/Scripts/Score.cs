using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector] public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            if (score > GameManager.instance.highScore)
            {
                GameManager.instance.highScore = score;
            }
        }
    }
}
