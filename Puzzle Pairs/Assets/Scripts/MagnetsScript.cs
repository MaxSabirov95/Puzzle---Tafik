﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MagnetsScript : MonoBehaviour
{
    public enum magnetType { male,female};
    public enum magnetPosition { up, down,right,left,notMovable };
    public magnetType magnet;
    public magnetPosition position;

    private Vector2 startPosition;

    //[SerializeField] Animator anim;
    [SerializeField] bool playerIn;
    [SerializeField] bool isWall;

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public float speed;
    public static int male;
    public static int female;
    public bool maleFemale;
    //public ParticleSystem effect;

    public GameObject placeEffect;
    public SpriteRenderer usbIn;
    public SpriteRenderer usbOut;

    public bool ifChipBroken;

    void Awake()
    {
        startPosition = transform.localPosition;
    }
    void Start()
    {
        if (usbIn == null && usbOut == null)
        {
            usbIn = null;
            usbOut = null;
        }
        else
        {
            usbIn.enabled = false;
            usbOut.enabled = true;
        }
        
    }
    void OnEnable()
    {
        transform.localPosition = startPosition;
        BlackBoard.magnet = this;
        Physics.IgnoreLayerCollision(11, 9);
        Physics.IgnoreLayerCollision(13, 13);
    }
    void FixedUpdate()
    {
        if ((male >= 1) && (female >= 1))
        {
            maleFemale = true;
        }
        else
        {
            maleFemale = false;
        }
        if (!Curser.dragging && !BlackBoard.scenesManager.ifWin && !ifChipBroken)
        {
            if (playerIn && !isWall)
            {
               // anim.SetBool("isOpen", true);
               // anim.SetBool("Close", false);
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
              // anim.SetBool("Close", false);
               // anim.SetBool("isOpen", false);
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPosition, speed);
            }
        }
        else
        {
           // anim.SetBool("Close", true);
           // anim.SetBool("isOpen", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
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
                    usbIn.enabled = true;
                    usbOut.enabled = false;
                    if (!Curser.dragging)
                    {
                        if(placeEffect != null)
                        {
                           // effect.transform.position = placeEffect.transform.position;
                            //effect.Play();
                            
                        }
                        BlackBoard.soundsManager.SoundsList(2);
                    }
                }
                else if(collision.CompareTag("red"))
                {
                    if (!Curser.dragging)
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
                else if (collision.CompareTag("blue"))
                {
                    if (!Curser.dragging)
                    {
                        BlackBoard.soundsManager.SoundsList(0);
                    }
                }
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            isWall = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("B.R"))
        {
            playerIn = false;
        }
        if (collision.CompareTag("Wall"))
        {
            isWall = false;
        }
        switch (magnet)
        {
            case magnetType.male:
                if (collision.CompareTag("blue"))
                {
                    female--;
                    usbIn.enabled = false;
                    usbOut.enabled = true;
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
}
