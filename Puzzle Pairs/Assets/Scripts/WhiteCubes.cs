using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    // bool dragging = false;
    [SerializeField] SpriteRenderer greenLight;
    [SerializeField] SpriteRenderer redLight;
    //public GameObject[] emptySlot;
    public bool inRange;
    bool canBePlaced = false;
    public List<GridTileCubes> greenSlots;
    public SpriteRenderer[] imagesLayers;

    private void Start()
    {
        BlackBoard._whiteCube = this;
        //emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
        //for (int i = 0; i < imagesLayers.Length; i++)
        //{
        //    imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        //}
    }
    private void Update()
    {
        if (BlackBoard.curser.dragging)
        {
            for (int i = 0; i < imagesLayers.Length; i++)
            {
                imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Drag");
            }
            greenLight.enabled = false;
            redLight.enabled = true;
        }
        else
        {
            for (int i = 0; i < imagesLayers.Length; i++)
            {
                imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
            }
        }
        //if (inRange && BlackBoard.curser.whiteCubes.Count <= 2)
        //{

        //    {
        //        if (!dragging && BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count <= 1 && BlackBoard.curser.howMuchInRange == 2)
        //        {
        //            foreach (GameObject slot in emptySlot)
        //            {
        //                if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 1f &&
        //                    Mathf.Abs(transform.position.y - slot.transform.position.y) <= 1f)
        //                {
        //                    slot.GetComponent<GridTileCubes>().isFull = false;
        //                    break;
        //                }
        //            }
        //            transform.SetParent(player.transform);
        //            BlackBoard.curser.whiteCubes.Add(this);
        //            dragging = true;
        //            BlackBoard.soundsManager.SoundsList(4);

        //        }
        //        else if(dragging && BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count > 0)
        //        {
        //            foreach (GameObject slot in emptySlot)
        //            {
        //                if ((BlackBoard.curser.whiteCubes.Count == 2))
        //                {
        //                    if (BlackBoard.curser.whiteCubes[0].canBePlaced && BlackBoard.curser.whiteCubes[1].canBePlaced)
        //                    {
        //                        if (Mathf.Abs(BlackBoard.curser.whiteCubes[1].transform.position.x - slot.transform.position.x) <= 1f &&
        //                            Mathf.Abs(BlackBoard.curser.whiteCubes[1].transform.position.y - slot.transform.position.y) <= 1f)
        //                        {
        //                            BlackBoard.curser.whiteCubes[1].transform.position = slot.transform.position;
        //                            BlackBoard.curser.whiteCubes[1].transform.parent = null;
        //                            foreach (GridTileCubes green in greenSlots)
        //                            {
        //                                if (BlackBoard.curser.whiteCubes[1].transform.position == green.transform.position)
        //                                {
        //                                    BlackBoard.curser.whiteCubes[1].greenLight.enabled = true;
        //                                    BlackBoard.curser.whiteCubes[1].redLight.enabled = false;
        //                                    break;
        //                                }
        //                            }
        //                            BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[1]);
        //                            StartCoroutine(waitToGrab());
        //                            slot.GetComponent<GridTileCubes>().isFull = true;
        //                            for (int i = 0; i < imagesLayers.Length; i++)
        //                            {
        //                                imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        //                            }
        //                            return;
        //                        }
        //                    }
        //                }
        //                if(BlackBoard.curser.whiteCubes.Count == 1)
        //                {
        //                    if (BlackBoard.curser.whiteCubes[0].canBePlaced)
        //                    {
        //                        if (Mathf.Abs(BlackBoard.curser.whiteCubes[0].transform.position.x - slot.transform.position.x) <= 1f &&
        //                            Mathf.Abs(BlackBoard.curser.whiteCubes[0].transform.position.y - slot.transform.position.y) <= 1f)
        //                        {
        //                            BlackBoard.curser.whiteCubes[0].transform.position = slot.transform.position;
        //                            BlackBoard.curser.whiteCubes[0].transform.parent = null;
        //                            foreach (GridTileCubes green in greenSlots)
        //                            {
        //                                if (BlackBoard.curser.whiteCubes[0].transform.position == green.transform.position)
        //                                {
        //                                    BlackBoard.curser.whiteCubes[0].greenLight.enabled = true;
        //                                    BlackBoard.curser.whiteCubes[0].redLight.enabled = false;
        //                                    break;
        //                                }
        //                            }
        //                            BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[0]);
        //                            BlackBoard.soundsManager.SoundsList(1);
        //                            StartCoroutine(waitToGrab());
        //                            slot.GetComponent<GridTileCubes>().isFull = true;

        //                            bool isAllGreenFull = true;
        //                            foreach (GridTileCubes green in greenSlots)
        //                            {
        //                                if (!green.isFull)
        //                                {
        //                                    isAllGreenFull = false;
        //                                }
        //                            }
        //                            if (isAllGreenFull)
        //                            {
        //                                Debug.Log("You Won");
        //                                //BlackBoard.scenesManager.NextLevel();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }

        if (col.gameObject.CompareTag("Empty Slot"))
        {
            canBePlaced = !col.GetComponent<GridTileCubes>().isFull;
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
        BlackBoard.curser.dragging = false;
    }

    void TEMP()
    {
        //if (inRange && BlackBoard.curser.whiteCubes.Count <= 2)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        if (!dragging && BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count <= 1 && BlackBoard.curser.howMuchInRange == 2)
        //        {
        //            foreach (GameObject slot in emptySlot)
        //            {
        //                if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 1f &&
        //                    Mathf.Abs(transform.position.y - slot.transform.position.y) <= 1f)
        //                {
        //                    slot.GetComponent<GridTileCubes>().isFull = false;
        //                    break;
        //                }
        //            }
        //            transform.SetParent(player.transform);
        //            BlackBoard.curser.whiteCubes.Add(this);
        //            dragging = true;
        //            BlackBoard.soundsManager.SoundsList(4);
        //            greenLight.enabled = false;
        //            redLight.enabled = true;
        //            for (int i = 0; i < imagesLayers.Length; i++)
        //            {
        //                imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Drag");
        //            }
        //        }
        //        else if(dragging && BlackBoard.magnet.maleFemale && BlackBoard.curser.whiteCubes.Count > 0)
        //        {
        //            foreach (GameObject slot in emptySlot)
        //            {
        //                if ((BlackBoard.curser.whiteCubes.Count == 2))
        //                {
        //                    if (BlackBoard.curser.whiteCubes[0].canBePlaced && BlackBoard.curser.whiteCubes[1].canBePlaced)
        //                    {
        //                        if (Mathf.Abs(BlackBoard.curser.whiteCubes[1].transform.position.x - slot.transform.position.x) <= 1f &&
        //                            Mathf.Abs(BlackBoard.curser.whiteCubes[1].transform.position.y - slot.transform.position.y) <= 1f)
        //                        {
        //                            BlackBoard.curser.whiteCubes[1].transform.position = slot.transform.position;
        //                            BlackBoard.curser.whiteCubes[1].transform.parent = null;
        //                            foreach (GridTileCubes green in greenSlots)
        //                            {
        //                                if (BlackBoard.curser.whiteCubes[1].transform.position == green.transform.position)
        //                                {
        //                                    BlackBoard.curser.whiteCubes[1].greenLight.enabled = true;
        //                                    BlackBoard.curser.whiteCubes[1].redLight.enabled = false;
        //                                    break;
        //                                }
        //                            }
        //                            BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[1]);
        //                            StartCoroutine(waitToGrab());
        //                            slot.GetComponent<GridTileCubes>().isFull = true;
        //                            for (int i = 0; i < imagesLayers.Length; i++)
        //                            {
        //                                imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        //                            }
        //                            return;
        //                        }
        //                    }
        //                }
        //                if(BlackBoard.curser.whiteCubes.Count == 1)
        //                {
        //                    if (BlackBoard.curser.whiteCubes[0].canBePlaced)
        //                    {
        //                        if (Mathf.Abs(BlackBoard.curser.whiteCubes[0].transform.position.x - slot.transform.position.x) <= 1f &&
        //                            Mathf.Abs(BlackBoard.curser.whiteCubes[0].transform.position.y - slot.transform.position.y) <= 1f)
        //                        {
        //                            BlackBoard.curser.whiteCubes[0].transform.position = slot.transform.position;
        //                            BlackBoard.curser.whiteCubes[0].transform.parent = null;
        //                            foreach (GridTileCubes green in greenSlots)
        //                            {
        //                                if (BlackBoard.curser.whiteCubes[0].transform.position == green.transform.position)
        //                                {
        //                                    BlackBoard.curser.whiteCubes[0].greenLight.enabled = true;
        //                                    BlackBoard.curser.whiteCubes[0].redLight.enabled = false;
        //                                    break;
        //                                }
        //                            }
        //                            BlackBoard.curser.whiteCubes.Remove(BlackBoard.curser.whiteCubes[0]);
        //                            BlackBoard.soundsManager.SoundsList(1);
        //                            StartCoroutine(waitToGrab());
        //                            slot.GetComponent<GridTileCubes>().isFull = true;
        //                            for (int i = 0; i < imagesLayers.Length; i++)
        //                            {
        //                                imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        //                            }
        //                            bool isAllGreenFull = true;
        //                            foreach (GridTileCubes green in greenSlots)
        //                            {
        //                                if (!green.isFull)
        //                                {
        //                                    isAllGreenFull = false;
        //                                }
        //                            }
        //                            if (isAllGreenFull)
        //                            {
        //                                Debug.Log("You Won");
        //                                //BlackBoard.scenesManager.NextLevel();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
