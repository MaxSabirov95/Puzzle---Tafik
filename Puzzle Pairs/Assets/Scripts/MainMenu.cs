using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject levelButton;
    public Transform contentToLevelButtons;
    public int numberOfLevels;

    public GameObject levelsPanel;
    public GameObject mainMenuPanel;

    private void Start()
    {
        MainMenuPanelOn();
        for (int i = 0; i < numberOfLevels; i++)
        {
            GameObject go = Instantiate(levelButton, contentToLevelButtons);
            Button myBtn = go.GetComponent<Button>();
            myBtn.GetComponentInChildren<Text>().text = (i+1).ToString();
        }
    }

    public void EnterToLevelsPanel()
    {
        LevelsPanelOn();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void MainMenuPanelOn()
    {
        mainMenuPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }
    void LevelsPanelOn()
    {
        levelsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
