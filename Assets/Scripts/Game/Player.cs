using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity += new Vector2(horizontal, 0) * Time.deltaTime * speed;

    }
}
