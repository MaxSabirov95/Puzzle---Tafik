using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MagnetsScript : MonoBehaviour
{
    public enum magnetType { red,blue};
    public magnetType magnet;
    Vector3 StartPosition;

    public float timeMovement;

    public bool canMove;

    bool _magnet;
    bool _player;

    void Start()
    {
        StartPosition = new Vector3(transform.position.x, transform.position.y,0);
        BlackBoard.magnet = this;
        Physics.IgnoreLayerCollision(11, 9);
        //Physics.IgnoreLayerCollision(11, 10);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                else
                {
                    canMove = false;
                }
                break;
            case magnetType.blue:
                if (collision.CompareTag("red"))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
