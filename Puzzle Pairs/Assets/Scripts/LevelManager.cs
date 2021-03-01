using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool is4x5;
    public bool is5x5;
    public bool is6x5;

    private void OnEnable()
    {
        BlackBoard.scenesManager.board4x5.SetActive(false);
        BlackBoard.scenesManager.board5x5.SetActive(false);
        BlackBoard.scenesManager.board6x5.SetActive(false);

        if (is4x5)
        {
            BlackBoard.scenesManager.board4x5.SetActive(true);
        }
        else if (is5x5)
        {
            BlackBoard.scenesManager.board5x5.SetActive(true);
        }
        else if (is6x5)
        {
            BlackBoard.scenesManager.board6x5.SetActive(true);
        }
    }
}
