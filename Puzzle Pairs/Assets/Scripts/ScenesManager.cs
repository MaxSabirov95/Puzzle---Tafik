
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    Scene sceneLoaded;
    int sceneNumber;

    private int playerActions;
    [SerializeField] Text playerActionsText;

    public bool ifWin;
    public float time;
    [SerializeField] Text timerText;

    public GameObject winPanel;
    [SerializeField] Text _levelNumberText;
    [SerializeField] Text _timerText;
    [SerializeField] Text _actionText;

    private void Start()
    {
        playerActionsText.text = playerActions.ToString();
        sceneLoaded = SceneManager.GetActiveScene();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        BlackBoard.scenesManager = this;
        levelNumberText.text = "Level "+ (sceneNumber + 1);
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
        SceneManager.LoadScene(sceneLoaded.buildIndex);
    }

    public void ExitLevel()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        Reset();
        BlackBoard.magnet.Male_And_Female_Reset();
        SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
    }

    public void PreviousLevel()
    {
        Reset();
        BlackBoard.magnet.Male_And_Female_Reset();
        SceneManager.LoadScene(sceneLoaded.buildIndex - 1);
    }

    private void Reset()
    {
        playerActions = 0;
        time = 0;
        ifWin = false;
    }

    public void PlayerMoves()
    {
        playerActions++;
        playerActionsText.text = playerActions.ToString();
    }
}
