using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    Scene sceneLoaded;
    //int sceneNumber;
    public GameObject[] cubes;
    public GameObject[] cursers;
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
        //sceneNumber = SceneManager.GetActiveScene().buildIndex;
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
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        cursers = GameObject.FindGameObjectsWithTag("Player");
        //SceneManager.LoadScene(sceneLoaded.buildIndex);
        foreach (GameObject cube in cubes)
        {
            if (cube.activeInHierarchy)
            {
                cube.GetComponent<WhiteCubes>().RestartPosition();
            }
        }
        foreach (GameObject curser in cursers)
        {
            if (curser.activeInHierarchy)
            {
                curser.GetComponent<Curser>().RestartLevel();
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
}
