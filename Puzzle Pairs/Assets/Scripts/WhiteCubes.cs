﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    public bool dragging = false;
    public GameObject[] emptySlot;
    bool inRange;
    bool canBePlaced = false;
    public List<GridTileCubes> greenSlots;

    private void Start()
    {
        BlackBoard._whiteCube = this;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
    }
    private void Update()
    {
        if ((inRange) && BlackBoard.curser.whiteCubes.Count <= 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!dragging && BlackBoard.magnet.redBlue && BlackBoard.curser.whiteCubes.Count <= 1)
                {
                    transform.SetParent(player.transform);
                    BlackBoard.curser.whiteCubes.Add(this);
                    dragging = true;
                    BlackBoard.soundsManager.SoundsList(4);
                }
                else if(dragging && BlackBoard.magnet.redBlue && BlackBoard.curser.whiteCubes.Count > 0)
                {
                    foreach (GameObject slot in emptySlot)
                    {
                        if ((BlackBoard.curser.whiteCubes.Count == 2))
                        {
                            if (BlackBoard.curser.whiteCubes[0].canBePlaced && BlackBoard.curser.whiteCubes[1].canBePlaced)
                            {
                                if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 1f &&
                                    Mathf.Abs(transform.position.y - slot.transform.position.y) <= 1f)
                                {
                                    transform.position = slot.transform.position;
                                    BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[1]);
                                    transform.parent = null;
                                    dragging = false;
                                    slot.GetComponent<GridTileCubes>().isFull = true;
                                    return;
                                }
                            }
                        }
                        if(BlackBoard.curser.whiteCubes.Count == 1)
                        {
                            if (BlackBoard.curser.whiteCubes[0].canBePlaced)
                            {
                                if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 1f &&
                                    Mathf.Abs(transform.position.y - slot.transform.position.y) <= 1f)
                                {
                                    transform.position = slot.transform.position;
                                    BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[0]);
                                    transform.parent = null;
                                    BlackBoard.soundsManager.SoundsList(1);
                                    StartCoroutine(waitToGrab());
                                    slot.GetComponent<GridTileCubes>().isFull = true;
                                    bool isAllGreenFull = true;
                                    foreach (GridTileCubes green in greenSlots)
                                    {
                                        if (!green.isFull)
                                        {
                                            isAllGreenFull = false;
                                        }
                                    }
                                    if (isAllGreenFull)
                                    {
                                        Debug.Log("You Won");
                                    }
                                }
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
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
             inRange = false;
        }

        if (col.gameObject.CompareTag("Empty Slot"))
        {
            canBePlaced = false;
        }
    }

    IEnumerator waitToGrab()
    {
        yield return new WaitForSeconds(0.25f);
        dragging = false;
    }
}
