using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    public bool dragging=false;
    public GameObject[] emptySlot;
    Vector2 position;
    //Quaternion rotation;
    bool inRange;
    public bool canBePlaced = false;


    private void Start()
    {
        BlackBoard._whiteCube = this;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
    }
    private void Update()
    {
        if ((inRange) && BlackBoard.curser.howMuchRed==2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!dragging)
                {
                    //rotation = transform.rotation;
                    position = transform.position;
                    transform.SetParent(player.transform);
                    BlackBoard.curser.whiteCubes.Add(this);
                    dragging = true;
                    BlackBoard.curser.beDraged = true;
                }
                else
                {
                    foreach (GameObject slot in emptySlot)
                    {
                        if(BlackBoard.curser.whiteCubes[0].canBePlaced && BlackBoard.curser.whiteCubes[1].canBePlaced)
                        {
                            if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 0.5f &&
                            Mathf.Abs(transform.position.y - slot.transform.position.y) <= 0.5f)
                            {
                                transform.position = slot.transform.position;
                                transform.parent = null;
                                dragging = false;
                                BlackBoard.curser.beDraged = false;
                                BlackBoard.curser.whiteCubes.Remove(this);
                                slot.GetComponent<GridTileCubes>().isFull = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Empty Slot"))
        {
            if (!col.GetComponent<GridTileCubes>().isFull)
            {
                canBePlaced = true;
            }
            else
            {
                canBePlaced = false;
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

        //if (col.gameObject.CompareTag("Empty Slot"))
        //{
        //    canBePlaced = false;
        //}
    }
}
