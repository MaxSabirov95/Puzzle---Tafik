using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    public float rotation;
    public List<WhiteCubes> whiteCubes;
    public int howMuchInRange;
    public GameObject[] emptySlot;
    public List<GridTileCubes> greenSlots;
    public GameObject[] cubes;
    public bool dragging = false;
    bool moveDone;

    private void Start()
    {
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
        cubes = GameObject.FindGameObjectsWithTag("whiteCube");
        BlackBoard.curser = this;
        //Physics.IgnoreLayerCollision(10, 12);//--check what his mission
    }
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        transform.position = pos;

        if (Input.GetMouseButtonDown(1))
        {
            rotation -= 90;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            BlackBoard.soundsManager.SoundsList(3);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!dragging && BlackBoard.magnet.maleFemale && howMuchInRange == 2)
            {
                BlackBoard.soundsManager.SoundsList(4);
                foreach (GameObject whiteCube in cubes)
                {
                    WhiteCubes white_cube = whiteCube.GetComponent<WhiteCubes>();
                    if (white_cube.inRange)
                    {
                        whiteCube.transform.SetParent(transform);
                        whiteCubes.Add(white_cube);
                        white_cube.greenLight.enabled = false;
                        white_cube.redLight.enabled = true;
                        for (int i = 0; i < white_cube.imagesLayers.Length; i++)
                        {
                            white_cube.imagesLayers[i].sortingLayerID = SortingLayer.NameToID("Drag");
                        }
                        foreach (GameObject slot in emptySlot)
                        {
                            if (Mathf.Abs(whiteCube.transform.position.x - slot.transform.position.x) <= 1f &&
                                Mathf.Abs(whiteCube.transform.position.y - slot.transform.position.y) <= 1f)
                            {
                                slot.GetComponent<GridTileCubes>().isFull = false;
                                break;
                            }
                        }
                    }
                }
                dragging = true;
            }
            else if (dragging && BlackBoard.magnet.maleFemale)
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
                                whiteCubes[0].transform.parent = null;
                                whiteCubes.Remove(whiteCubes[0]);
                                slot.GetComponent<GridTileCubes>().isFull = true;
                                    if (whiteCubes.Count == 0)
                                    {
                                        BlackBoard.soundsManager.SoundsList(1);
                                        StartCoroutine(waitToGrab());
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
                                            //BlackBoard.scenesManager.NextLevel();
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
        dragging = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange++;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange--;
        }
    }
}


