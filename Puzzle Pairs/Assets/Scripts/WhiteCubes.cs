using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
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
    public List<GridTileCubes> greenSlots;


    private void Start()
    {
        BlackBoard._whiteCube = this;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
    }
    private void Update()
    {
        if ((inRange) &&  BlackBoard.curser.whiteCubes.Count <= 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!dragging && BlackBoard.magnet.redBlue && BlackBoard.curser.whiteCubes.Count <= 1)
                {
                    //rotation = transform.rotation;
                    position = transform.position;
                    transform.SetParent(player.transform);
                    BlackBoard.curser.whiteCubes.Add(this);
                    dragging = true;
                }
                else
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
                                    transform.parent = null;
                                    StartCoroutine(waitToGrab());
                                    BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[1]);
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
                                    transform.parent = null;
                                    StartCoroutine(waitToGrab());
                                    BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[0]);
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
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
             inRange = false;
        }
    }

    IEnumerator waitToGrab()
    {
        yield return new WaitForSeconds(0.25f);
        dragging = false;
    }
}
