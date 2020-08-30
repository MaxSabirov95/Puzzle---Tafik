using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MagnetsScript : MonoBehaviour
{
    [SerializeField] Animator animMale;
    [SerializeField] Animator animFemale;

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
    //float speedTemp;
    public static int male;
    public static int female;
    public bool maleFemale;
    public ParticleSystem effect;


    [SerializeField] bool playerIn;

    void Start()
    {
        //speedTemp = speed;
        if (animMale == null)
        {
            animMale = null;
        }
        if (animFemale == null)
        {
            animFemale = null;
        }
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
                if(animMale != null)
                {
                    animMale.SetBool("isOpen", true);
                    animMale.SetBool("Close", false);
                }
                if (animFemale != null)
                {
                    animFemale.SetBool("open", true);
                }

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
                if (animMale != null)
                {
                    animMale.SetBool("Close", false);
                    animMale.SetBool("isOpen", false);
                }
                if (animFemale != null)
                {
                    animFemale.SetBool("open", false);
                }
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPosition, speed);
            }
        }
        else
        {
            if (animMale != null)
            {
                animMale.SetBool("Close", true);
            }
            if (animFemale != null)
            {
                animFemale.SetBool("open", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
            //speed = speedTemp;
        }
        //else if (collision.CompareTag("M.R"))
        //{
        //    playerIn = true;
        //    speed /= 100;
        //}

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
        if (collision.CompareTag("B.R"))
        {
            //speed = speedTemp;
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
