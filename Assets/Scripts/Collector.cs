using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{

    [SerializeField] Collectible[] _collectibles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_collectibles.Any(t => t.gameObject.activeSelf == true)) //Any uses System.Linq, if any item in the array matches the statement, if any items in the array return true, this statement will also return true
        {
            return;        
        }
        Debug.Log("Got all gems.");
    }
}
