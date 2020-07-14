using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    public float rotation;
    public GameObject[] cubes;
    public WhiteCubes[] whiteCubes;
    public static int i = 0;
    public bool canDrag;

    private void Start()
    {
        BlackBoard.curser = this;
    }
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,1));
        transform.position = pos;

        if (Input.GetMouseButtonDown(1))
        {
            rotation -= 90;
            transform.rotation = Quaternion.Euler(0,0, rotation);
        }

        foreach (WhiteCubes item in whiteCubes)
        {
            if (item.dragging)
            {

            }
        }
    }
}
