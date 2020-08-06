using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileCubes : MonoBehaviour
{
    public bool isFull;

    [SerializeField]
    private int whiteCubesInRange;

    public GameObject[] whiteCubes;

    private void Start()
    {
        BlackBoard.gridTileCubes = this;
        whiteCubes = GameObject.FindGameObjectsWithTag("whiteCube");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            whiteCubesInRange++;
        } 
    }
   
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            if (whiteCubesInRange >= 1)
            {
                whiteCubesInRange--;
                if (whiteCubesInRange == 0)
                {
                    isFull = false;
                }
            }
        }
    }
}
