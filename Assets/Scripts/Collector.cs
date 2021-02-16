using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{

    [SerializeField] Collectible[] _collectibles;
    TMP_Text _remainingCounter;

    // Start is called before the first frame update
    void Start()
    {
        _remainingCounter = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

        int countRemaining = 0; //
        foreach (var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled)
            {
                countRemaining++;
            }
        }

        _remainingCounter?.SetText(countRemaining.ToString()); //Set the counter's text to our remaining amount of collectibles -- the ? is shorthand for != null (evaluates true)

        if (countRemaining > 0)
        {
            return;
        }
        //if (_collectibles.Any(t => t.gameObject.activeSelf == true)) //Any uses System.Linq, if any item in the array matches the statement, if any items in the array return true, this statement will also return true
        //{
          //  return;        
        //}
        Debug.Log("Got all gems.");
    }
}
