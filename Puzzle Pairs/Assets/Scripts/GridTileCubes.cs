using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileCubes : MonoBehaviour
{

    [SerializeField] int whiteCubesInRange;

    public bool isFull;

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
