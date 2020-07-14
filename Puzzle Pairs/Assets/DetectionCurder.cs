using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCurder : MonoBehaviour
{
    public bool canMove;

    private void Start()
    {
        BlackBoard.detectionCurder = this;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canMove = true;
            Debug.Log(canMove);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        canMove = false;
    }
}
