using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetsScript : MonoBehaviour
{
    public enum magnetType { male,female};
    public enum magnetPosition { up, down,right,left,notMovable };
    public magnetType magnet;
    public magnetPosition position;


    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    private Vector2 startPosition;
    public float speed;
    public static int male;
    public static int female;
    public bool maleFemale;
    public ParticleSystem effect;


    private bool playerIn;

    //public enum maleSides { right, left };
    //public maleSides MaleSides;
    //private Vector2 startRotation;

    void Start()
    {
        startPosition = transform.localPosition;
        //startRotation = transform.eulerAngles;
        BlackBoard.magnet = this;
        Physics.IgnoreLayerCollision(11, 9);
        Physics.IgnoreLayerCollision(13, 13);
    }

    private void Update()
    {
        Debug.Log(male);
        Debug.Log(female);
        if ((male >= 1) && (female >= 1))
        {
            maleFemale = true;
        }
        else
        {
            maleFemale = false;
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

                //switch (MaleSides)
                //{
                //    case maleSides.right:
                //        transform.localPosition = Vector2.MoveTowards(right.transform.localPosition, right.transform.localPosition, speed);
                //        break;
                //    case maleSides.left:
                //        transform.localPosition = Vector2.MoveTowards(left.transform.localPosition, left.transform.localPosition, speed);
                //        break;
                //}
            }
            else
            {
                transform.localPosition = Vector2.MoveTowards(startPosition, startPosition, speed);
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
            case magnetType.male:
                if (collision.CompareTag("blue"))
                {
                    female++;
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
            case magnetType.female:
                if (collision.CompareTag("red"))
                {
                    male++;
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
            case magnetType.male:
                if (collision.CompareTag("blue"))
                {
                    female--;
                }
                break;
            case magnetType.female:
                if (collision.CompareTag("red"))
                {
                    male--;
                }
                break;
        }
    }

    public void ResetOrNextLevel()
    {
        male = 0;
        female = 0;
    }
}
