using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; } //Can be gotten from wherever, but only set inside

    void Awake()
    {
        if (Instance == null) // Singleton pattern
        {
            Instance = this; // Reference to this object
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
