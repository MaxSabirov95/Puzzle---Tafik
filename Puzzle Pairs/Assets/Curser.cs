using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    public float rotation;
    public List <WhiteCubes> whiteCubes;
    public bool canMoveCube;
    public bool beDraged;
    public bool red;
    public bool blue;
    //public int howMuchRed = 0;

    private void Start()
    {
        Cursor.visible = false;
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


        if (red && blue)
        {
            canMoveCube = true;
        }
        else
        {
            canMoveCube = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!beDraged)
        {
            if (col.gameObject.CompareTag("red"))
            {
                //howMuchRed++;
                red = true;
            }
            if (col.gameObject.CompareTag("blue"))
            {
                blue = true;
            }
        }

        
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!beDraged)
        {
            if (col.gameObject.CompareTag("red"))
            {
                //howMuchRed--;
                red = false;
            }
            if (col.gameObject.CompareTag("blue"))
            {
                blue = false;
            }
        }
    }
}
