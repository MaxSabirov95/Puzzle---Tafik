﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_Rotate : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(0,0,speed);
    }
}
