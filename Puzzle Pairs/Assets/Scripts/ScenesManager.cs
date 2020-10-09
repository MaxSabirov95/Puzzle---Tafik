using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] Text levelNumber;

    Scene sceneLoaded;
    int sceneNumber;
    private void Start()
    {
        sceneLoaded = SceneManager.GetActiveScene();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        BlackBoard.scenesManager = this;
        levelNumber.text = "Level "+ (sceneNumber + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneLoaded.buildIndex);
    }

    public void ExitLevel()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        BlackBoard.magnet.Male_And_Female_Reset();
        SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
    }

    public void PreviousLevel()
    {
        BlackBoard.magnet.Male_And_Female_Reset();
        SceneManager.LoadScene(sceneLoaded.buildIndex - 1);
    }
}
