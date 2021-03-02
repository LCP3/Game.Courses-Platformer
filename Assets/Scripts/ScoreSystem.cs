using System;
using UnityEngine;


public static class ScoreSystem
{
    public static event Action<int> OnScoreChange;
    static int _score;
    static int _highScore;

    public static void Add(int points)
    {
        _score += points;
        OnScoreChange?.Invoke(_score);



        if (_score > _highScore) // If the score is higher than the current high score
        {
            _highScore = _score; // Set new high score

            PlayerPrefs.SetInt("highScore", _highScore); // Set a key in PlayerPrefs for a persistent high score.
        }
    }
}
