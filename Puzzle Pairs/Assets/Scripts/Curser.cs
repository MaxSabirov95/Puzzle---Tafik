using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Curser : MonoBehaviour
{
    public float rotation;
    private bool Rotation;
    public List<WhiteCubes> whiteCubes;
    public int howMuchInRange;
    public GameObject[] emptySlot;
    public List<GridTileCubes> greenSlots;
    public GameObject[] cubes;
    public bool dragging = false;
    public bool ifWall;// bool to walls
    private bool moveDone;

    int cubesOnSamePositions;

    private void Start()
    {
        ifWall = false;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        BlackBoard.curser = this;
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
                "time", 0.2f,
                "easetype", iTween.EaseType.linear
                ));
            BlackBoard.soundsManager.SoundsList(3);
            StartCoroutine(CurserRotation());
        }

        if (Input.GetMouseButtonDown(0) && !BlackBoard.scenesManager.ifWin && !ifWall && !Rotation)
        {
            if (!dragging && BlackBoard.magnet.maleFemale && howMuchInRange == 2)
            {
                BlackBoard.soundsManager.SoundsList(4);
                foreach (GameObject whiteCube in cubes)
                {
                    if (whiteCube.GetComponent<WhiteCubes>().inRange)
                    {
                        whiteCubes.Add(whiteCube.GetComponent<WhiteCubes>());
                        whiteCube.GetComponent<WhiteCubes>().transform.SetParent(transform);
                        whiteCube.GetComponent<WhiteCubes>().greenLight.enabled = false;
                        whiteCube.GetComponent<WhiteCubes>().redLight.enabled = true;
                        whiteCube.GetComponent<WhiteCubes>().draging = true;
                        for (int j = 0; j < whiteCube.GetComponent<WhiteCubes>().imagesLayers.Length; j++)
                        {
                            whiteCube.GetComponent<WhiteCubes>().imagesLayers[j].sortingLayerID = SortingLayer.NameToID("Drag");
                        }
                        foreach (GameObject slot in emptySlot)
                        {
                            if (Mathf.Abs(whiteCube.GetComponent<WhiteCubes>().transform.position.x - slot.transform.position.x) <= 1f &&
                                Mathf.Abs(whiteCube.GetComponent<WhiteCubes>().transform.position.y - slot.transform.position.y) <= 1f)
                            {
                                slot.GetComponent<GridTileCubes>().isFull = false;
                                break;
                            }
                        }
                    }
                    if(whiteCubes.Count == 2)
                    {
                        dragging = true;
                        break;
                    }
                }
            }
            else if (dragging && BlackBoard.magnet.maleFemale && whiteCubes.Count==2)
            {
                if (whiteCubes[0].canBePlaced && whiteCubes[1].canBePlaced)
                {
                    for (int j = 0; j < 2; j++)//--check how much cubes left in list
                    {
                        foreach (GameObject slot in emptySlot)
                        {
                            if (whiteCubes.Count > 0)
                            {
                                if (Mathf.Abs(whiteCubes[0].transform.position.x - slot.transform.position.x) <= 1f &&
                                    Mathf.Abs(whiteCubes[0].transform.position.y - slot.transform.position.y) <= 1f)
                                {
                                    whiteCubes[0].transform.position = slot.transform.position;
                                    if(whiteCubes[0].transform.position == whiteCubes[0].positionTemp)
                                    {
                                        cubesOnSamePositions++;
                                    }
                                    
                                    whiteCubes[0].positionTemp = whiteCubes[0].transform.position;

                                    for (int i = 0; i < whiteCubes[0].imagesLayers.Length; i++)
                                    {
                                        whiteCubes[0].imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
                                    }

                                    foreach (GridTileCubes green in greenSlots)
                                    {
                                        if (whiteCubes[0].transform.position == green.transform.position)
                                        {
                                            whiteCubes[0].greenLight.enabled = true;
                                            whiteCubes[0].redLight.enabled = false;
                                        }
                                    }

                                    if (whiteCubes.Count == 1)
                                    {
                                        if (cubesOnSamePositions <= 1)
                                        {
                                            foreach (GameObject cube in cubes)
                                            {
                                                cube.GetComponent<WhiteCubes>().CubesActionAfterPlayerAction();
                                            }
                                        }
                                    }
                                   //whiteCubes[0].draging = false;
                                    whiteCubes[0].transform.parent = null;
                                    whiteCubes.Remove(whiteCubes[0]);
                                    slot.GetComponent<GridTileCubes>().isFull = true;
                                    if (whiteCubes.Count == 0)
                                    {
                                        foreach (GameObject cube in cubes)
                                        {
                                            cube.GetComponent<WhiteCubes>().draging=false;
                                        }
                                        BlackBoard.soundsManager.SoundsList(1);
                                        StartCoroutine(waitToGrab());
                                        cubesOnSamePositions = 0;
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
                                            BlackBoard.scenesManager.ifWin=true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
        }      
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
}


