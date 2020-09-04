using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer greenLight;
    public SpriteRenderer redLight;
    public bool inRange;
    public bool canBePlaced = false;
    public SpriteRenderer[] imagesLayers;

    private void Start()
    {
        BlackBoard._whiteCube = this;

        for (int i = 0; i < imagesLayers.Length; i++)
        {
            imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        }
    }
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        col.GetComponent<Curser>().whiteCubes.Add(this);
    //    }

    //}
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
            //col.GetComponent<Curser>().whiteCubes.Remove(this);
        }

        if (col.gameObject.CompareTag("Empty Slot"))
        {
            canBePlaced = false;
        }
    }
}
