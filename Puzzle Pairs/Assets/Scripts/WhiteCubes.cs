using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    public bool dragging = false;
    [SerializeField] SpriteRenderer greenLight;
    [SerializeField] SpriteRenderer redLight;
    public GameObject[] emptySlot;
    bool inRange;
    bool canBePlaced = false;
    public List<GridTileCubes> greenSlots;

    private void Start()
    {
        greenLight.enabled = false;
        redLight.enabled = true;
        BlackBoard._whiteCube = this;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
    }
    private void Update()
    {
        if (inRange && BlackBoard.curser.whiteCubes.Count <= 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!dragging && BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count <= 1 && (BlackBoard.curser.howMuchInRange == 2))
                {
                    foreach (GameObject slot in emptySlot)
                    {
                        if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 1f &&
                            Mathf.Abs(transform.position.y - slot.transform.position.y) <= 1f)
                        {
                            slot.GetComponent<GridTileCubes>().isFull = false;
                        }
                    }
                    transform.SetParent(player.transform);
                    BlackBoard.curser.whiteCubes.Add(this);
                    dragging = true;
                    BlackBoard.soundsManager.SoundsList(4);
                }
                else if(dragging && BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count > 0)
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
                                    StartCoroutine(waitToGrab());
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
                                        BlackBoard.scenesManager.NextLevel();
                                    }
                                }
                            }
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

        if (col.gameObject.CompareTag("Female"))
        {
            if (BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count <= 1)
            {
                greenLight.enabled = true;
                redLight.enabled = false;
            }
            else if(BlackBoard.magnet.maleFemale && dragging)
            {
                greenLight.enabled = true;
                redLight.enabled = false;
            }
            else
            {
                greenLight.enabled = false;
                redLight.enabled = true;
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

        if (col.gameObject.CompareTag("Female"))
        {
            greenLight.enabled = false;
            redLight.enabled = true;
        }
    }

    IEnumerator waitToGrab()
    {
        yield return new WaitForSeconds(0.25f);
        dragging = false;
    }
}
