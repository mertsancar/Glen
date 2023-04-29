using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public bool isStealth = false;

    [Header("Jump Controller")]
    [SerializeField] private int jumpPower;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    private void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.1f, 0.11f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        var horizontal = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity += new Vector2(horizontal, 0) * Time.deltaTime * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "House")
        {
            isStealth = true;
            var houseSprite = collision.GetComponent<SpriteRenderer>();
            houseSprite.color = new Color(houseSprite.color.r, houseSprite.color.g, houseSprite.color.b, .75f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "House")
        {
            isStealth = false;
            var houseSprite = collision.GetComponent<SpriteRenderer>();
            houseSprite.color = new Color(houseSprite.color.r, houseSprite.color.g, houseSprite.color.b, 1f);
        }
    }

}
