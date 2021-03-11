using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    [SerializeField] Text playerActionsText;
    [SerializeField] Text _levelNumberText;
    [SerializeField] Text _timerText;
    [SerializeField] Text _actionText;
    string textTime;

    public static int levelsNow=0;
    public int playerActions;   

    public bool ifWin;
    public float time;
    
    public GameObject winPanel;
    public GameObject[] cubes;
    public GameObject[] wallFlipers;
    public GameObject[] levels;

    public GameObject board2x2;
    public GameObject board4x5;
    public GameObject board5x5;
    public GameObject board6x5;

    public Button nextButton;
    public Button previousButton;

    private void Awake()
    {
        BlackBoard.scenesManager = this;
    }

    void OnEnable()
    {
        Next_PreviousButtons();
        levelsNow = LevelButton.levelsNum;
        playerActionsText.text = "Moves: " + playerActions.ToString();        
        levels[levelsNow - 1].SetActive(true);
        levelNumberText.text = "Level "+ (levelsNow);
        levelsNow--;
        winPanel.SetActive(false);
    }
    void Update()
    {
        if (!ifWin)
        {
            time = time + Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time - minutes * 60f);
            textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            winPanel.SetActive(true);
            _levelNumberText.text = levelNumberText.text+ " Completed";
            _actionText.text = "You Did "+ playerActionsText.text + " Moves";
            _timerText.text = "Your Time " + textTime;

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
        RestartLevel();
        levels[levelsNow].SetActive(false);
        BlackBoard.mainMenu.ToMainMenu();
    }

    public void Next_PreviousLevel(bool isNext)
    {
        RestartLevel();
        levels[levelsNow].SetActive(false);
        if (isNext)
        {
            levelsNow++;
            if (levelsNow + 1 > levels.Length)
            {
                levelsNow--;
            }
            winPanel.SetActive(false);
        }
        else
        {
            levelsNow--;
            if (levelsNow < 0)
            {
                levelsNow = 0;
            }
        }
        levels[levelsNow].SetActive(true);
        levelNumberText.text = "Level " + (levelsNow + 1);
        BlackBoard.curser.RestartLevel();
        Next_PreviousButtons();
    }

    public void PlayerMoves()
    {
        playerActions++;
        playerActionsText.text = "Moves: " + playerActions.ToString();
    }

    public void Next_PreviousButtons()
    {
        
        if (levelsNow == 0)
        {
            previousButton.interactable=false;
        }
        else
        {
            previousButton.interactable = true;
        }

        if (BlackBoard.mainMenu.isNextLevelOpen[levelsNow+1] == 1)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }
}
