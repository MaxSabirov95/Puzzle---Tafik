using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MagnetsScript : MonoBehaviour
{
    public enum magnetType { red,blue};
    public magnetType magnet;

    void Start()
    {
        BlackBoard.magnet = this;
        Physics.IgnoreLayerCollision(11, 9);
    }
}
