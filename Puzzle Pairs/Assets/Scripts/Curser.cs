using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Curser : MonoBehaviour
{
    private bool Rotation;
    private float distance = 1.5f;

    public List<WhiteCubes> whiteCubes;
    public GameObject[] emptySlot;
    public GameObject[] greenSlots;
    public GameObject[] cubes;

    public int howMuchInRange;
    public bool ifWall;// bool to walls
    public int totalCubes=2;
    public int cubesOnSamePositions;

    public static bool dragging = false;

    void Awake()
    {
        BlackBoard.curser = this;
    }
    void Start()
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
                int cubeNumberInList = 0;
                BlackBoard.soundsManager.SoundsList(4);
                foreach (GameObject whiteCube in cubes)
                {
                    if (whiteCube.GetComponent<WhiteCubes>().inRange)
                    {
                        whiteCubes.Add(whiteCube.GetComponent<WhiteCubes>());
                        whiteCubes[cubeNumberInList].transform.SetParent(transform);
                        whiteCubes[cubeNumberInList].greenLight.enabled = false;
                        whiteCubes[cubeNumberInList].redLight.enabled = true;
                        whiteCubes[cubeNumberInList].draging = true;
                        for (int j = 0; j < whiteCubes[cubeNumberInList].imagesLayers.Length; j++)
                        {
                            whiteCubes[cubeNumberInList].imagesLayers[j].sortingLayerID = SortingLayer.NameToID("Drag");
                        }
                        foreach (GameObject slot in emptySlot)
                        {
                            if (Mathf.Abs(whiteCubes[cubeNumberInList].transform.position.x - slot.transform.position.x) <= distance &&
                                Mathf.Abs(whiteCubes[cubeNumberInList].transform.position.y - slot.transform.position.y) <= distance)
                            {
                                slot.GetComponent<GridTileCubes>().isFull = false;
                                break;
                            }
                        }
                        foreach (GameObject green in greenSlots)
                        {
                            if (Mathf.Abs(whiteCubes[cubeNumberInList].transform.position.x - green.transform.position.x) <= distance &&
                                Mathf.Abs(whiteCubes[cubeNumberInList].transform.position.y - green.transform.position.y) <= distance)
                            {
                                green.GetComponent<GridTileCubes>().isFull = false;
                                break;
                            }
                        }
                        cubeNumberInList++;
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
                            ReleaseCubes(slot);
                        }//--Check if cube land on brown slot
                    }
                    if (whiteCubes.Count > 0)
                    {
                        foreach (GameObject green in greenSlots)
                        {
                            ReleaseCubes(green);
                        }//--Check if cube land on green slot
                    }
                }//--Just in case if curser will grab 1 cube
            }//--Grab cubes
            else if (dragging && BlackBoard.magnet.maleFemale && whiteCubes.Count == totalCubes)
            {
                if (whiteCubes[0].canBePlaced && whiteCubes[1].canBePlaced)
                {
                    for (int j = 0; j < totalCubes; j++)//--check how much cubes left in list
                    {
                        foreach (GameObject slot in emptySlot)
                        {
                            ReleaseCubes(slot);
                        }//--Check if cube land on brown slot
                        foreach (GameObject green in greenSlots)
                        {
                            ReleaseCubes(green);
                        }//--Check if cube land on green slot
                    }
                    if (cubesOnSamePositions <= 1)
                    {
                        foreach (GameObject cube in cubes)
                        {
                            cube.GetComponent<WhiteCubes>().CubesActionAfterPlayerAction();
                            cube.GetComponent<WhiteCubes>().draging = false;
                        }
                    }
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange++;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            ifWall = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
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

    public void ReleaseCubes(GameObject cube)
    {
        if (whiteCubes.Count >= 1)
        {
                if (Vector2.Distance(whiteCubes[0].transform.position, cube.transform.position) <= distance)
            {
                whiteCubes[0].transform.position = cube.transform.position;
                cube.GetComponent<GridTileCubes>().isFull = true;
                whiteCubes[0].IsGreenLight();
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
            }
        }
    }
}


