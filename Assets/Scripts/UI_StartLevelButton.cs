using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartLevelButton : MonoBehaviour
{
    [SerializeField] string _levelName;

    public string LevelName => _levelName;  //Public get-only property, expression body syntax equivalent public string LevelName { get { return _levelName; } }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName); // Load a level defined in the inspector
    }
}
