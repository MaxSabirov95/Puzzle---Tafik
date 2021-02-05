using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class MainMenu : MonoBehaviour
{
    public GameObject levelButton;
    public Transform contentToLevelButtons;
    public int numberOfLevels;

    public GameObject levelsPanel;
    public GameObject mainMenuPanel;


    public int[] movesInLevels;
    public int[] isNextLevelOpen; // 1=ture   0=false

    private void Awake()
    {
        BlackBoard.mainMenu = this;
    }

    private void Start()
    {
        
        movesInLevels = new int[numberOfLevels];
        isNextLevelOpen = new int[numberOfLevels];
        MainMenuPanelOn();
        for (int i = 0; i < numberOfLevels; i++)
        {
            try
            {
                LoadStats(i);
            }
            catch (NullReferenceException)
            { 
                SaveMoves(i, 0);
                SaveOpenLevel(i, 0);
            }
            GameObject go = Instantiate(levelButton, contentToLevelButtons);
            Button myBtn = go.GetComponent<Button>();
            myBtn.GetComponentInChildren<Text>().text = (i + 1).ToString();
            if (isNextLevelOpen[i] == 0)
            {
                if (i != 0)
                {
                    myBtn.interactable = false;
                } // first level always open
            }
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

    public void SaveMoves(int level, int moves)
    {
        PlayerPrefs.SetInt("Level" + level, moves);
        Debug.Log("Save Moves " + moves);
    } // save player moves

    public void SaveOpenLevel(int level, int ifOpenNextLevel)
    {
        PlayerPrefs.SetInt("Next Level Open?" + level, ifOpenNextLevel);
        Debug.Log("Next Level Open? " + (level+1) +" " +ifOpenNextLevel);
    }// save and check if next level need to be open

    public void LoadStats(int level)
    {
        movesInLevels[level] = PlayerPrefs.GetInt("Level" + level);
        isNextLevelOpen[level] = PlayerPrefs.GetInt("Next Level Open?" + level);
        Debug.Log("Level "+ (level+1) +" Load Moves " + movesInLevels[level]);
    }// load stats
}
