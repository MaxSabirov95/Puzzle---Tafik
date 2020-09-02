using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    Scene sceneLoaded;
    private void Start()
    {
        sceneLoaded = SceneManager.GetActiveScene();
        BlackBoard.scenesManager = this;
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
