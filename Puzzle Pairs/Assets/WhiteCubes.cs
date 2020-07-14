using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCubes : MonoBehaviour
{
    public GameObject player;
    public bool dragging=false;
    public GameObject[] emptySlot;

    private void Start()
    {
        BlackBoard._whiteCube = this;
        emptySlot = GameObject.FindGameObjectsWithTag("Empty Slot");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((!dragging)&&(BlackBoard.magnet.canMove))
                {
                    transform.SetParent(player.transform);
                    dragging = true;
                }
                else
                {
                    foreach (GameObject slot in emptySlot)
                    {
                        if (Mathf.Abs(transform.position.x - slot.transform.position.x) <= 0.5f&&
                            Mathf.Abs(transform.position.y - slot.transform.position.y) <= 0.5f)
                        {
                                transform.position = slot.transform.position;
                                transform.parent = null;
                                dragging = false; 
                        }
                    }
                    
                }
            }
        }
    }
}
