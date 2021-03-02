using UnityEngine;

public class UILockable : MonoBehaviour
{ 
    private void OnEnable()
        {

            var startButton = GetComponent<UI_StartLevelButton>();
            string key = startButton.LevelName + "Unlocked"; // "Level1Unlocked"
            int unlocked = PlayerPrefs.GetInt(key, 0); //PlayerPrefs are a common way to store persistent data (does user have level unlocked)

            if (unlocked == 0)
                gameObject.SetActive(false);
        }

    [ContextMenu("Clear Unlocks")]
    void ClearUnlockedLevels()
    {
        var startButton = GetComponent<UI_StartLevelButton>();
        string key = startButton.LevelName + "Unlocked"; // "Level1Unlocked"
        PlayerPrefs.DeleteKey(key);
    }
}