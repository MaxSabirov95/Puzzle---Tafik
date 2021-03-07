using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public bool is2x2;
    public bool is4x5;
    public bool is5x5;
    public bool is6x5;

    private void OnEnable()
    {
        BlackBoard.scenesManager.board2x2.SetActive(false);
        BlackBoard.scenesManager.board4x5.SetActive(false);
        BlackBoard.scenesManager.board5x5.SetActive(false);
        BlackBoard.scenesManager.board6x5.SetActive(false);

        if (is2x2)
        {
            BlackBoard.scenesManager.board2x2.SetActive(true);
        }
        else if (is4x5)
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
