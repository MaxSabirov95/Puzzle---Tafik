using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileCubes : MonoBehaviour
{
    public enum kindOfCube { green,red};
    public kindOfCube cubeKind;
    public bool isFull;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //switch (cubeKind)
        //{
        //    case kindOfCube.green:
        //        //if (col.CompareTag("whiteCube"))
        //        //{
        //        //    isFull = true;
        //        //}
        //        break;
        //    case kindOfCube.red:
        //        if (col.CompareTag("whiteCube"))
        //        {
        //            isFull = true;
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        switch (cubeKind)
        {
            case kindOfCube.green:
                //if (col.CompareTag("whiteCube"))
                //{
                //    isFull = true;
                //}
                break;
            case kindOfCube.red:
                if (col.CompareTag("whiteCube"))
                {
                    isFull = false;
                }
                break;
            default:
                break;
        }
    }
}
