using TMPro;
using UnityEngine;

public class UIHighScore : MonoBehaviour
{
    TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        int _highScore = PlayerPrefs.GetInt("highScore");

        _text.SetText("High Score: " + _highScore.ToString());
    }

    [ContextMenu("Reset High Score")]
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("highScore");
    }
}
