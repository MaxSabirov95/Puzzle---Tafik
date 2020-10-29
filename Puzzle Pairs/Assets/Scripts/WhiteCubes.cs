using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public enum KindOfCube { Normal,Flip}
    public KindOfCube kindOfCube;
    public bool draging;
    public SpriteRenderer greenLight;
    public SpriteRenderer redLight;
    public bool inRange;
    public bool canBePlaced = false;
    public SpriteRenderer[] imagesLayers;
    public List<GridTileCubes> greenSlots;
    [HideInInspector]
    public Vector3 positionTemp;

    private void Start()
    {
        positionTemp = transform.position;
        BlackBoard._whiteCube = this;

        for (int i = 0; i < imagesLayers.Length; i++)
        {
            imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        }

        foreach (GridTileCubes green in greenSlots)
        {
            if (transform.position == green.transform.position)
            {
                greenLight.enabled = true;
                redLight.enabled = false;
                break;
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
    public void CubesActionAfterPlayerAction()
    {
        switch (kindOfCube)
        {
            case KindOfCube.Flip:
                if (!draging)
                {
                    //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z-90);
                    iTween.RotateTo(this.gameObject, iTween.Hash(
                          "rotation", new Vector3(0, 0, transform.rotation.eulerAngles.z - 90),
                          "time", 0.2f,
                          "easetype", iTween.EaseType.easeInBack
                ));
                }
                break;
        }
    }
}
