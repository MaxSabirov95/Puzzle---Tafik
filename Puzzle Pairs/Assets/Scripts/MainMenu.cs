using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using GameAnalyticsSDK;


public class MainMenu : MonoBehaviour
{
    public GameObject levelButton;
    public Transform contentToLevelButtons;
    public int numberOfLevels;

    public GameObject levelsPanel;
    public GameObject mainMenuPanel;
    public GameObject levelsManager;
    public GameObject optionsPanel;

    public int[] movesInLevels;
    public int[] isNextLevelOpen; // 1=ture   0=false

    bool isLevelsOn = false;
    bool isOptionsOn = false;
    public Text stars;

    private void Awake()
    {
        BlackBoard.mainMenu = this;
    }

    private void Start()
    {
        GameAnalytics.Initialize();
        movesInLevels = new int[numberOfLevels];
        isNextLevelOpen = new int[numberOfLevels];
        levelsPanel.SetActive(false);
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
            
            myBtn.GetComponent<LevelButton>().levelNum = i + 1;
            if (isNextLevelOpen[i] == 0)
            {
                if (i != 0)
                {
                    myBtn.interactable = false;
                    myBtn.GetComponentInChildren<Text>().text = "";
                    myBtn.GetComponent<Image>().sprite = myBtn.GetComponent<LevelButton>().close;
                } // first level always open
                else
                {
                    myBtn.GetComponentInChildren<Text>().text = (i + 1).ToString();
                    myBtn.GetComponent<Image>().sprite = myBtn.GetComponent<LevelButton>().available;
                }
            }
            else 
            {
                myBtn.GetComponentInChildren<Text>().text = (i + 1).ToString();
                myBtn.GetComponent<Image>().sprite = myBtn.GetComponent<LevelButton>().available;
            }
        }
        stars.text = BlackBoard.goalsInLevel.stars.ToString();
    }

    public void EnterToLevelsPanel()
    {
        if (isOptionsOn)
        {
            optionsPanel.SetActive(false);
            isOptionsOn = false;
        }
        levelsPanel.SetActive(isLevelsOn = !isLevelsOn);
    }

    public void OptionsPanelOn()
    {
        if (isLevelsOn)
        {
            levelsPanel.SetActive(false);
            isLevelsOn = false;
        }
        optionsPanel.SetActive(isOptionsOn = !isOptionsOn);
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
        BlackBoard.goalsInLevel.CalculateStars(movesInLevels[level],level);
        isNextLevelOpen[level] = PlayerPrefs.GetInt("Next Level Open?" + level);
    }// load stats

    public void EnterToLevel()
    {
        levelsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        levelsManager.SetActive(true);
    }

    public void ToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelsManager.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
