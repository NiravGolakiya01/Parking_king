using UnityEngine;
using UnityEngine.SceneManagement;


public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject levelBtn;
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject Play;
    [SerializeField] GameObject GoHome;

    public void levelManu()
    {
        levelPanel.SetActive(true);
    }

    public void Info()
    {
        infoPanel.SetActive(true);
    }

    public void Close()
    {
        levelPanel.SetActive(false); 
        infoPanel.SetActive(false); 
    }

    public void startGame()
    {
        SceneManager.LoadSceneAsync("Level-1");
    }

    public void goHome()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
