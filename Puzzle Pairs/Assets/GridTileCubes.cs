using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileCubes : MonoBehaviour
{
    public enum kindOfCube { green,red};
    public kindOfCube cubeKind;
    public bool isFull;
    public int i;
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
            if((transform.position.x == white.transform.position.x)&&(transform.position.y == white.transform.position.y))
            {
                isFull = true;
                break;
            }
            else
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
        if (i >= 1)
        {
            switch (cubeKind)
            {
                case kindOfCube.green:
                    if (col.CompareTag("whiteCube"))
                    {
                        i--;
                        if (i == 0)
                        {
                            isFull = false;
                        }
                    }
                    break;
                case kindOfCube.red:
                    if (col.CompareTag("whiteCube"))
                    {
                        i--;
                        if (i == 0)
                        {
                            isFull = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
