using System;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChange;

    public static int Score { get; private set; }

    static int _highScore;    

    void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        Score = 0;
    }
    public static void Add(int points)
    {
        Score += points;
        OnScoreChange?.Invoke(Score);



        if (Score > _highScore) // If the score is higher than the current high score
        {
            _highScore = Score; // Set new high score

            PlayerPrefs.SetInt("HighScore", _highScore); // Set a key in PlayerPrefs for a persistent high score.
        }
    }
}
