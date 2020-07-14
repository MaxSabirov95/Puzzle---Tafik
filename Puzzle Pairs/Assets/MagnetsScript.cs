using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetsScript : MonoBehaviour
{
    public enum magnetType { red,blue};
    public magnetType magnet;
    Vector3 StartPosition;
    Vector3 targetPosition;
    public float timeMovement;

    public bool canMove;
    void Start()
    {
        StartPosition = new Vector3(transform.position.x, transform.position.y,0);
        BlackBoard.magnet = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (magnet)
        {
            case magnetType.red:
                if (collision.CompareTag("blue"))
                {
                    canMove = true;
                }
                break;
            case magnetType.blue:
                if (collision.CompareTag("red"))
                {
                    canMove = true;
                }
                break;

        }
    }
    }
