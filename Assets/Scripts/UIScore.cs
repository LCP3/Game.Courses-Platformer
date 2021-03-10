using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoreSystem.OnScoreChange += UpdateScoreText; //When score changes, update the score text -- event registration
    }

    void OnDestroy()
    {
        ScoreSystem.OnScoreChange -= UpdateScoreText; //Deregister
    }

    private void UpdateScoreText(int score)
    {
        Debug.Log("Score updated to " + score);
        _text.SetText(score.ToString()); //Set text to score value
    }
}