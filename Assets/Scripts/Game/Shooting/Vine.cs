using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareVine()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.160518f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.24f, 2.321035f);
    }


}
