using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Curser : MonoBehaviour
{
    public float rotation;
    private bool Rotation;
    public List<WhiteCubes> whiteCubes;
    public int howMuchInRange;
    public GameObject[] emptySlot;
    public GameObject[] greenSlots;
    public GameObject[] cubes;
    public static bool dragging = false;
    public bool ifWall;// bool to walls
    //public Transform _parent;
    public int totalCubes=2;
    float distanse = 1.5f;

    public int cubesOnSamePositions;
    
    void Awake()
    {
        BlackBoard.curser = this;
    }
    private void Start()
    {
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        greenSlots = GameObject.FindGameObjectsWithTag("Green Slot");
    }
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        transform.position = pos;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13f, 13f), Mathf.Clamp(transform.position.y, -6f, 10f), transform.position.z);

        if (Input.GetMouseButtonDown(1) && dragging && !Rotation)
        {
            Rotation = true;
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "rotation", new Vector3(0, 0, transform.rotation.eulerAngles.z - 90),
                "time", 0.15f,
                "easetype", iTween.EaseType.linear
                ));
            BlackBoard.soundsManager.SoundsList(3);
            StartCoroutine(CurserRotation());
        }//--Rotate curser

        else if (Input.GetMouseButtonDown(0) && !BlackBoard.scenesManager.ifWin && !ifWall && !Rotation)
        {
            if (!dragging && BlackBoard.magnet.maleFemale && howMuchInRange == 2)
            {
                int a = 0;
                BlackBoard.soundsManager.SoundsList(4);
                foreach (GameObject whiteCube in cubes)
                {
                    if (whiteCube.GetComponent<WhiteCubes>().inRange)
                    {
                        whiteCubes.Add(whiteCube.GetComponent<WhiteCubes>());
                        whiteCubes[a].transform.SetParent(transform);
                        whiteCubes[a].greenLight.enabled = false;
                        whiteCubes[a].redLight.enabled = true;
                        whiteCubes[a].draging = true;
                        for (int j = 0; j < whiteCubes[a].imagesLayers.Length; j++)
                        {
                            whiteCubes[a].imagesLayers[j].sortingLayerID = SortingLayer.NameToID("Drag");
                        }
                        foreach (GameObject slot in emptySlot)
                        {
                            if (Mathf.Abs(whiteCubes[a].transform.position.x - slot.transform.position.x) <= distanse &&
                                Mathf.Abs(whiteCubes[a].transform.position.y - slot.transform.position.y) <= distanse)
                            {
                                slot.GetComponent<GridTileCubes>().isFull = false;
                                break;
                            }
                        }
                        foreach (GameObject green in greenSlots)
                        {
                            if (Mathf.Abs(whiteCubes[a].transform.position.x - green.transform.position.x) <= distanse &&
                                Mathf.Abs(whiteCubes[a].transform.position.y - green.transform.position.y) <= distanse)
                            {
                                green.GetComponent<GridTileCubes>().isFull = false;
                                break;
                            }
                        }
                        a++;
                    }
                    if (whiteCubes.Count == totalCubes)
                    {
                        dragging = true;
                        break;
                    }
                }

                if (whiteCubes.Count < totalCubes)
                {
                    Debug.Log("IN");
                    if (whiteCubes.Count > 0)
                    {
                        foreach (GameObject slot in emptySlot)
                        {
                            if (Mathf.Abs(whiteCubes[0].transform.position.x - slot.transform.position.x) <= 1f &&
                                    Mathf.Abs(whiteCubes[0].transform.position.y - slot.transform.position.y) <= 1f)
                            {
                                whiteCubes[0].transform.position = slot.transform.position;
                                slot.GetComponent<GridTileCubes>().isFull = true;
                                if (whiteCubes[0].transform.position == whiteCubes[0].positionTemp)
                                {
                                    cubesOnSamePositions++;
                                }//--Check if cube land on same position as before

                                whiteCubes[0].positionTemp = whiteCubes[0].transform.position;

                                for (int i = 0; i < whiteCubes[0].imagesLayers.Length; i++)
                                {
                                    whiteCubes[0].imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
                                }//--Change all sprites from drag to NotDrag  

                                whiteCubes[0].transform.SetParent(whiteCubes[0].parent);
                                break;
                            }
                        }
                    }
                    if (whiteCubes.Count > 0)
                    {
                        foreach (GameObject green in greenSlots)
                        {
                            if (Mathf.Abs(whiteCubes[0].transform.position.x - green.transform.position.x) <= 1f &&
                                    Mathf.Abs(whiteCubes[0].transform.position.y - green.transform.position.y) <= 1f)
                            {
                                whiteCubes[0].transform.position = green.transform.position;
                                green.GetComponent<GridTileCubes>().isFull = true;
                                whiteCubes[0].greenLight.enabled = true;
                                whiteCubes[0].redLight.enabled = false;
                                if (whiteCubes[0].transform.position == whiteCubes[0].positionTemp)
                                {
                                    cubesOnSamePositions++;
                                }//--Check if cube land on same position as before

                                whiteCubes[0].positionTemp = whiteCubes[0].transform.position;

                                for (int i = 0; i < whiteCubes[0].imagesLayers.Length; i++)
                                {
                                    whiteCubes[0].imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
                                }//--Change all sprites from drag to NotDrag  

                                whiteCubes[0].transform.SetParent(whiteCubes[0].parent);
                                break;
                            }

                        }//--Check if cube land on green slot
                    }
                    whiteCubes.Clear();
                }//--Just in case if curser will grab 1 cube
            }//--Grab cubes
            else if (dragging && BlackBoard.magnet.maleFemale && whiteCubes.Count == totalCubes)
            {
                if (whiteCubes[0].canBePlaced && whiteCubes[1].canBePlaced)
                {
                    for (int j = 0; j < totalCubes; j++)//--check how much cubes left in list
                    {
                        if (whiteCubes.Count > 0)
                        {
                            foreach (GameObject slot in emptySlot)
                            {
                            if (Mathf.Abs(whiteCubes[0].transform.position.x - slot.transform.position.x) <= distanse &&
                                    Mathf.Abs(whiteCubes[0].transform.position.y - slot.transform.position.y) <= distanse)
                            {
                                whiteCubes[0].transform.position = slot.transform.position;
                                slot.GetComponent<GridTileCubes>().isFull = true;
                                if (whiteCubes[0].transform.position == whiteCubes[0].positionTemp)
                                {
                                    cubesOnSamePositions++;
                                }//--Check if cube land on same position as before

                                whiteCubes[0].positionTemp = whiteCubes[0].transform.position;

                                for (int i = 0; i < whiteCubes[0].imagesLayers.Length; i++)
                                {
                                    whiteCubes[0].imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
                                }//--Change all sprites from drag to NotDrag  

                                whiteCubes[0].transform.SetParent(whiteCubes[0].parent);
                                whiteCubes.Remove(whiteCubes[0]);
                                break;
                            }
                        }
                        }   
                        if (whiteCubes.Count > 0)
                        {
                            foreach (GameObject green in greenSlots)
                            {
                                if (Mathf.Abs(whiteCubes[0].transform.position.x - green.transform.position.x) <= distanse &&
                                        Mathf.Abs(whiteCubes[0].transform.position.y - green.transform.position.y) <= distanse)
                                {
                                    whiteCubes[0].transform.position = green.transform.position;
                                    green.GetComponent<GridTileCubes>().isFull = true;
                                    whiteCubes[0].greenLight.enabled = true;
                                    whiteCubes[0].redLight.enabled = false;
                                    if (whiteCubes[0].transform.position == whiteCubes[0].positionTemp)
                                    {
                                        cubesOnSamePositions++;
                                    }//--Check if cube land on same position as before

                                    whiteCubes[0].positionTemp = whiteCubes[0].transform.position;

                                    for (int i = 0; i < whiteCubes[0].imagesLayers.Length; i++)
                                    {
                                        whiteCubes[0].imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
                                    }//--Change all sprites from drag to NotDrag  

                                    whiteCubes[0].transform.SetParent(whiteCubes[0].parent);
                                    whiteCubes.Remove(whiteCubes[0]);
                                    break;
                                }

                            }//--Check if cube land on green slot
                        }
                    }

                    if (cubesOnSamePositions < totalCubes)
                    {
                        foreach (GameObject cube in cubes)
                        {
                            cube.GetComponent<WhiteCubes>().CubesActionAfterPlayerAction();
                            cube.GetComponent<WhiteCubes>().draging = false;
                        }
                    }//--Cubes action
                    if (whiteCubes.Count == 0)
                    {
                        BlackBoard.soundsManager.SoundsList(1);
                        if (WhiteCubes.isFlipSound)
                        {
                            StartCoroutine(redDelay());
                        }
                        StartCoroutine(waitToGrab());
                        cubesOnSamePositions = 0;
                        bool isAllGreenFull = true;
                        foreach (GameObject green in greenSlots)
                        {
                            if (!green.GetComponent<GridTileCubes>().isFull)
                            {
                                isAllGreenFull = false;
                                break;
                            }
                        }//--Check if all green slots are full
                        if (isAllGreenFull)
                        {
                            BlackBoard.scenesManager.ifWin = true;
                        }
                    }
                }

            }//--Put cubes
        }//--Grab and put cubes
    }

    IEnumerator waitToGrab()
    {
        yield return new WaitForSeconds(0.25f);
        BlackBoard.scenesManager.PlayerMoves();
        dragging = false;
    }
    IEnumerator CurserRotation()
    {
        yield return new WaitForSeconds(0.2f);
        Rotation = false;
    }
    IEnumerator redDelay()
    {
        yield return new WaitForSeconds(0.15f);
        BlackBoard.soundsManager.SoundsList(5);
        WhiteCubes.isFlipSound = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange++;
        }
        if (col.CompareTag("Wall"))
        {
            ifWall = true;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            ifWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange--;
        }
        if (col.CompareTag("Wall"))
        {
            ifWall = false;
        }
    }

    public void RestartLevel()
    {
        if (whiteCubes.Count > 0)
        {
            do
            {
                whiteCubes[0].transform.SetParent(whiteCubes[0].parent);
                whiteCubes.Remove(whiteCubes[0]);
            } while (whiteCubes.Count > 0);
        }
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        greenSlots = GameObject.FindGameObjectsWithTag("Green Slot");
        dragging = false;
        ifWall = false;
    }
}


