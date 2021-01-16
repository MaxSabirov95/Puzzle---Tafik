using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileCubes : MonoBehaviour
{
    public enum TargetSlots { Normal, Electro};
    public TargetSlots targetSlots;

    [SerializeField] int whiteCubesInRange;
    public GameObject triger;

    public bool isFull;
    public bool isAvailable = true;

    private void Start()
    {
        if (triger == null)
        {
            triger = null;
        }
    }
    void Update()
    {
        switch (targetSlots)
        {
            case TargetSlots.Electro:
                if (triger.GetComponent<GridTileCubes>().isFull)
                {
                    isAvailable = true;
                    this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
                else
                {
                    isAvailable = false;
                    this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                }
                break;
        } 
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            whiteCubesInRange++;
        } 
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("whiteCube"))
        {
            if (whiteCubesInRange >= 1)
            {
                whiteCubesInRange--;
                if (whiteCubesInRange == 0)
                {
                    isFull = false;
                }
            }
        }
    }
}
