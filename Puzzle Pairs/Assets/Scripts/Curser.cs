using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    public float rotation;
    public List <WhiteCubes> whiteCubes;
    public int howMuchInRange;

    private void Start()
    {
        BlackBoard.curser = this;
        Physics.IgnoreLayerCollision(10, 12);
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            howMuchInRange++;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            howMuchInRange--;
        }
    }
}
