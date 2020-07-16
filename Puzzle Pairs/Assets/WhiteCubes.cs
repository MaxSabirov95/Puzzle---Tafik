using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    public bool dragging=false;
    public GameObject[] emptySlot;
    Vector2 position;
    Quaternion rotation;
    bool inRange;


    private void Start()
    {
        BlackBoard._whiteCube = this;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
    }
    private void Update()
    {
        if ((inRange) && BlackBoard.curser.canMoveCube)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!dragging)
                {
                    rotation = transform.rotation;
                    position = transform.position;
                    transform.SetParent(player.transform);
                    dragging = true;
                    BlackBoard.curser.beDraged = true;
                }
                else
                {
                    foreach (GameObject slot in emptySlot)
                    {
                        if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 0.5f &&
                            Mathf.Abs(transform.position.y - slot.transform.position.y) <= 0.5f)
                        {
                            transform.position = slot.transform.position;
                            transform.parent = null;
                            dragging = false;
                            BlackBoard.curser.beDraged = false;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
             inRange = false;
        }
    }
}
