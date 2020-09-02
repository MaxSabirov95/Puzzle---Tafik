using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    public float rotation;
    public List <WhiteCubes> whiteCubes;
    public int howMuchInRange;
    public GameObject[] emptySlot;
    public GameObject[] cubes;
    public bool dragging = false;

    private void Start()
    {
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
        BlackBoard.curser = this;
        Physics.IgnoreLayerCollision(10, 12);//--check what his mission
    }
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,1));
        transform.position = pos;

        if (Input.GetMouseButtonDown(1))
        {
            rotation -= 90;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            BlackBoard.soundsManager.SoundsList(3);
        }







        if (Input.GetMouseButtonDown(0))
        {
            dragging = !dragging;
            if (dragging)
            {
                foreach (WhiteCubes whiteCube in whiteCubes)
                {
                    if (whiteCube.inRange)
                    {
                        whiteCube.transform.SetParent(transform);
                        whiteCubes.Add(whiteCube);
                    }
                }
            }
        }









    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange++;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))//--connector boxes
        {
            howMuchInRange--;
        }
    }
}
