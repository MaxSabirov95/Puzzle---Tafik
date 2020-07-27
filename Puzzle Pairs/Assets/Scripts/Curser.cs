using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    public float rotation;
    public List <WhiteCubes> whiteCubes;

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
            if (rotation <= -360)
            {
                rotation = 0;
            }
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            BlackBoard.soundsManager.SoundsList(3);
        }
    }
}
