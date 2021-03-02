using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] string _key;

    /*    TMP_Text _text;

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
}*/

    private void OnEnable()
    {
        int value = PlayerPrefs.GetInt(_key);
        GetComponent<TMP_Text>().SetText(value.ToString());
    }

    [ContextMenu("Reset High Score")]
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
