using TMPro;
using UnityEngine;

public class CoinsCollected : MonoBehaviour
{
    TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText(Coin.CoinsCollected.ToString());
    }
}
