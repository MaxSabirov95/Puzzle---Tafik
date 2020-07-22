using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitLevel()
    {
        Application.Quit();
    }
}
