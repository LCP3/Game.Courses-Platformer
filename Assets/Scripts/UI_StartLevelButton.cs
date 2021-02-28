using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartLevelButton : MonoBehaviour
{
    [SerializeField] string _levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName); // Load a level defined in the inspector
    }

    private void OnValidate()
    {
        GetComponentInChildren<TMP_Text>()?.SetText(_levelName); //Set the buttons' text to the level names on validation in the editor.
    }
}
