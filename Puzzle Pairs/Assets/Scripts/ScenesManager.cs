using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    Scene sceneLoaded;
    int sceneNumber;
    public GameObject[] cubes;
    private int playerActions;
    [SerializeField] Text playerActionsText;

    public bool ifWin;
    public float time;
    [SerializeField] Text timerText;

    public GameObject winPanel;
    [SerializeField] Text _levelNumberText;
    [SerializeField] Text _timerText;
    [SerializeField] Text _actionText;

    public GameObject[] levels;
    int levelsNow;

    private void Start()
    {
        levelsNow = 0;
        playerActionsText.text = playerActions.ToString();
        sceneLoaded = SceneManager.GetActiveScene();
        BlackBoard.scenesManager = this;
        levelNumberText.text = "Level "+ (levelsNow + 1);
        winPanel.SetActive(false);
    }

    private void Update()
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
        Reset();
        BlackBoard.curser.RestartLevel();
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        foreach (GameObject cube in cubes)
        {
            if (cube.activeInHierarchy)
            {
                cube.GetComponent<WhiteCubes>().RestartPosition();
            }
        }
        
    }

    public void ExitLevel()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        RestartLevel();
        levels[levelsNow].SetActive(false);
        levelsNow++;
        if (levelsNow+1 > levels.Length)
        {
            levelsNow--; 
        }
        levels[levelsNow].SetActive(true);
        levelNumberText.text = "Level " + (levelsNow + 1);
        winPanel.SetActive(false);
        BlackBoard.curser.RestartLevel();
        //SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
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
        // SceneManager.LoadScene(sceneLoaded.buildIndex - 1);
    }

    private void Reset()
    {
        playerActions = 0;
        time = 0;
        ifWin = false;
        playerActionsText.text = playerActions.ToString();
    }

    public void PlayerMoves()
    {
        playerActions++;
        playerActionsText.text = playerActions.ToString();
    }

    public void NextSection()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber + 1);
    }
    public void PreviousSection()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber - 1);
    }
}
