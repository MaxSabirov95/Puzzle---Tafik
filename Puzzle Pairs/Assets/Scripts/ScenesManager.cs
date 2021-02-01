using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    [SerializeField] Text playerActionsText;
    [SerializeField] Text _levelNumberText;
    [SerializeField] Text _timerText;
    [SerializeField] Text _actionText;
    [SerializeField] Text timerText;

    public static int levelsNow=0;
    private int playerActions;   

    public bool ifWin;
    public float time;
    public int[] nextSectionLevel;
    int section;
    
    public GameObject winPanel;
    public GameObject[] cubes;
    public GameObject[] wallFlipers;
    public GameObject[] levels;

    void Start()
    {
        levelsNow = LevelButton.levelsNum;
        playerActionsText.text = "Moves: " + playerActions.ToString();
        BlackBoard.scenesManager = this;
        levels[levelsNow-1].SetActive(true);
        levelNumberText.text = "Level "+ (levelsNow);
        winPanel.SetActive(false);
    }
    void Update()
    {
        
        if (!ifWin)
        {
            time = time + Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time - minutes * 60f);
            string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = textTime;
        }
        else
        {
            winPanel.SetActive(true);
            _levelNumberText.text = levelNumberText.text+ " Completed";
            _actionText.text = "You Did "+ playerActionsText.text + " Moves";
            _timerText.text = "Your Time "+timerText.text;

        }
    }

    public void RestartLevel()
    {
        playerActions = 0;
        time = 0;
        ifWin = false;
        playerActionsText.text = "Moves: " + playerActions.ToString();
        BlackBoard.curser.RestartLevel();
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        wallFlipers = GameObject.FindGameObjectsWithTag("Wall Fliper");
        foreach (GameObject cube in cubes)
        {
            if (cube.activeInHierarchy)
            {
                cube.GetComponent<WhiteCubes>().RestartPosition();
            }
        }
        foreach (GameObject wallFliper in wallFlipers)
        {
            if (wallFliper.activeInHierarchy)
            {
                wallFliper.GetComponent<WallFliper>().RestartPosition();
            }
        }
    }
    public void ExitLevel()
    {
        levels[levelsNow - 1].SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
    public void NextLevel()
    {
        RestartLevel();
        levels[levelsNow].SetActive(false);
        levelsNow++;
        if (levelsNow + 1 > levels.Length)
        {
            levelsNow--; 
        }
        levels[levelsNow].SetActive(true);
        levelNumberText.text = "Level " + (levelsNow + 1);
        winPanel.SetActive(false);
        BlackBoard.curser.RestartLevel();
    }
    public void PreviousLevel()
    {
        RestartLevel();
        
        levels[levelsNow].SetActive(false);
        levelsNow--;
        if (levelsNow < 0)
        {
            levelsNow = 0;
            
        }
        levels[levelsNow].SetActive(true);
        levelNumberText.text = "Level " + (levelsNow + 1);
        BlackBoard.curser.RestartLevel();
    }

    public void PlayerMoves()
    {
        playerActions++;
        playerActionsText.text = "Moves: " + playerActions.ToString();
    }
    public void NextSection()
    {
        section++;
        if (section >= nextSectionLevel.Length)
        {
            section--;
        }
        else
        {
            RestartLevel();
            levels[levelsNow].SetActive(false);
            levelsNow = nextSectionLevel[section] - 1;
            levels[nextSectionLevel[section]-1].SetActive(true);
            levelNumberText.text = "Level " + nextSectionLevel[section];
            BlackBoard.curser.RestartLevel();
        }
    }
    public void PreviousSection()
    {
        section--;
        if (section<0)
        {
            section++;
        }
        else
        {
            RestartLevel();
            levels[levelsNow].SetActive(false);
            levelsNow = nextSectionLevel[section] - 1;
            levels[nextSectionLevel[section]-1].SetActive(true);
            levelNumberText.text = "Level " + nextSectionLevel[section];
            BlackBoard.curser.RestartLevel();
        }
    }
}
