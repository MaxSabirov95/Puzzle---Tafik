using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public static int levelsNum;
    public void LevelButtonPress()
    {
        int levelNum = Convert.ToInt32(this.gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text);
        levelsNum = levelNum;
        SceneManager.LoadScene("Puzzle");
    }
}
