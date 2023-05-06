using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            GetComponent<Animator>().SetBool("Genarate", true);
            var rb = GetComponent<Rigidbody2D>();
            Destroy(rb);
        }
    }
}
