using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MagnetsScript : MonoBehaviour
{
    [SerializeField] Animator anim;

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
    public GameObject placeEffect;


    [SerializeField] bool playerIn;
    private void Awake()
    {
        startPosition = transform.localPosition;
    }
    void OnEnable()
    {
        transform.localPosition = startPosition;
        BlackBoard.magnet = this;
        Physics.IgnoreLayerCollision(11, 9);
        Physics.IgnoreLayerCollision(13, 13);
    }

    private void Update()
    {
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
        if (!Curser.dragging && !BlackBoard.scenesManager.ifWin)
        {
            if (playerIn)
            {
                anim.SetBool("isOpen", true);
                anim.SetBool("Close", false);
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
                anim.SetBool("Close", false);
                anim.SetBool("isOpen", false);
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPosition, speed);
            }
        }
        else
        {
            anim.SetBool("Close", true);
            anim.SetBool("isOpen", false);
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
                    if (!Curser.dragging)
                    {
                        if(placeEffect != null)
                        {
                            effect.transform.position = placeEffect.transform.position;
                            effect.Play();
                            BlackBoard.soundsManager.SoundsList(2);
                        }
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
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("B.R"))
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

    //public void Male_And_Female_Reset()
    //{
    //    male = 0;
    //    female = 0;
    //    transform.localPosition = startPosition;
    //}
}
