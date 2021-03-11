using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public enum KindOfCube { Normal,Flip,Broken}
    public KindOfCube kindOfCube;

    public SpriteRenderer greenLight;
    public SpriteRenderer redLight;
    public SpriteRenderer[] imagesLayers;
    public List<GridTileCubes> greenSlots;

    public bool draging;
    public bool isGreen;
    public bool inRange;
    public bool canBePlaced = false;
    public bool ifChipBroken;

    private Quaternion playerRotation;

    [HideInInspector]
    public Vector3 positionTemp;
    public Vector3 startPosition;
    public Transform parent;

    public static bool isFlipSound;

    void Awake()
    {
        playerRotation = transform.rotation;
        startPosition = transform.position;
    }
    void Start()
    {
        RestartPosition();
        positionTemp = transform.position;
        parent = GameObject.FindGameObjectWithTag("Parent").transform;

        for (int i = 0; i < imagesLayers.Length; i++)
        {
            imagesLayers[i].GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("NotDrag");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }

        if ((col.gameObject.CompareTag("Empty Slot") || col.gameObject.CompareTag("Green Slot")) && col.gameObject.GetComponent<GridTileCubes>().isAvailable)
        {
            canBePlaced = !col.GetComponent<GridTileCubes>().isFull;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }

        if (col.gameObject.CompareTag("Empty Slot") || col.gameObject.CompareTag("Green Slot"))
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
                    isFlipSound = true;
                    StartCoroutine(FlipDelay());
                }
                break;
        }
    }

    IEnumerator FlipDelay()
    {
        yield return new WaitForSeconds(0.15f);
        iTween.RotateTo(this.gameObject, iTween.Hash(
                          "rotation", new Vector3(0, 0, transform.rotation.eulerAngles.z - 90),
                          "time", 0.2f,
                          "easetype", iTween.EaseType.easeInBack
                ));
    }

    public void IsGreenLight()
    {
        if (!ifChipBroken)
        {
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
    }
    public void RestartPosition()
    {
        positionTemp = startPosition;
        draging = false;
        transform.position = startPosition;
        transform.rotation = playerRotation;
        greenLight.enabled = false;
        redLight.enabled = true;
        IsGreenLight();
    }
}
