using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButton;

    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); 
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        //for (int i = 0; i < unlockedLevel; i++)
        for (int i = 0; i < Mathf.Min(unlockedLevel, buttons.Length); i++) // Added bounds check
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level-" + levelId;
        SceneManager.LoadScene(levelName);
    }

    void ButtonsToArray()
    {
        int childCount = levelButton.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButton.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
