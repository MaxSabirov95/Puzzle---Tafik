using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MagnetsScript : MonoBehaviour
{
    public enum magnetType { red,blue};
    public enum magnetPosition { up, down,right,left };
    public magnetType magnet;
    public magnetPosition position;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    private Vector2 startPosition;
    public float speed;
    public static int red;
    public static int blue;
    public bool redBlue;
    public ParticleSystem effect;

    private bool playerIn;

    void Start()
    {
        startPosition = transform.localPosition;
        BlackBoard.magnet = this;
        Physics.IgnoreLayerCollision(11, 9);
    }

    private void Update()
    {
        if((red == 1) && (blue == 1))
        {
            redBlue = true;
        }
        else
        {
            redBlue = false;
        }
    }

    private void FixedUpdate()
    {
        if (!BlackBoard._whiteCube.dragging)
        {
            if (playerIn)
            {
                switch (position)
                {
                    case magnetPosition.up:
                        transform.localPosition = Vector2.MoveTowards(transform.localPosition, up.transform.localPosition, speed);
                        break;
                    case magnetPosition.down:
                        transform.localPosition = Vector2.MoveTowards(transform.localPosition, down.transform.localPosition, speed);
                        break;
                    case magnetPosition.right:
                        transform.localPosition = Vector2.MoveTowards(transform.localPosition, right.transform.localPosition, speed);
                        break;
                    case magnetPosition.left:
                        transform.localPosition = Vector2.MoveTowards(transform.localPosition, left.transform.localPosition, speed);
                        break;
                }
            }
            else
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPosition, speed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
        }

        switch (magnet)
        {
            case magnetType.red:
                if (collision.CompareTag("blue"))
                {
                    blue++;
                    if (!BlackBoard._whiteCube.dragging)
                    {
                        effect.transform.position = transform.position;
                        effect.Play();
                        BlackBoard.soundsManager.SoundsList(2);
                    }
                }
                else if(collision.CompareTag("red"))
                {
                    if (!BlackBoard._whiteCube.dragging)
                    {
                        BlackBoard.soundsManager.SoundsList(0);
                    }
                }
                break;
            case magnetType.blue:
                if (collision.CompareTag("red"))
                {
                    red++;
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = false;
        }

        switch (magnet)
        {
            case magnetType.red:
                if (collision.CompareTag("blue"))
                {
                    blue--;
                }
                break;
            case magnetType.blue:
                if (collision.CompareTag("red"))
                {
                    red--;
                }
                break;
        }
        
    }
}
