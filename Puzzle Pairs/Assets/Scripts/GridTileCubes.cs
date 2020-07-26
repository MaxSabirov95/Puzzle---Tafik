using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileCubes : MonoBehaviour
{
    public enum kindOfCube { green,red};
    public kindOfCube cubeKind;
    public bool isFull;

    [SerializeField]
    private int i;

    public GameObject[] whiteCubes;

    private void Start()
    {
        BlackBoard.gridTileCubes = this;
        whiteCubes = GameObject.FindGameObjectsWithTag("whiteCube");
    }
    private void Update()
    {
        foreach (GameObject white in whiteCubes)
        {
            if((transform.position.x == white.transform.position.x) && (transform.position.y == white.transform.position.y))
            {
                isFull = true;
                break;
            }
            if((transform.position.x != white.transform.position.x) || (transform.position.y != white.transform.position.y))
            {
                isFull = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            i++;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            if (i >= 1)
            {
                i--;
                if (i == 0)
                {
                    isFull = false;
                }
            }
        }
    }
}
