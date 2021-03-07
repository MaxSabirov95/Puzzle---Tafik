using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class LevelButton : MonoBehaviour
{
    public static int levelsNum;
    public int levelNum;
    public void LevelButtonPress()
    {
        levelsNum = levelNum;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, " ", levelsNum.ToString());
        //SceneManager.LoadScene("Puzzle");
        BlackBoard.mainMenu.EnterToLevel();
    }
}
