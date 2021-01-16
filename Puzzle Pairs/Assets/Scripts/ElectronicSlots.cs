using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicSlots : GridTileCubes
{
    public GameObject triggerSlot;

    void Update()
    {
        if (triggerSlot.GetComponent<GridTileCubes>().isFull)
        {
            isFull = false;
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        else
        {
            isFull = true;
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        }
    }
}
