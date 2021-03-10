using System;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChange;

    static int _score;
    static int _highScore;

    void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        _score = 0;
    }
    public static void Add(int points)
    {
        _score += points;
        OnScoreChange?.Invoke(_score);



        if (_score > _highScore) // If the score is higher than the current high score
        {
            _highScore = _score; // Set new high score

            PlayerPrefs.SetInt("HighScore", _highScore); // Set a key in PlayerPrefs for a persistent high score.
        }
    }
}
