using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public Animator animator;
    public int jumpPower;
    public bool isGrounded;
    public bool isStealth = false;
    
    private bool isFacingRight = true;
    private bool isCrouching = false;

    
    private void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        animator.SetBool("isRunning", Input.GetButton("Horizontal"));
        var horizontal = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y) ;
        ChangePlayerDirection(horizontal);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        
        // if (Input.GetKey(KeyCode.C))
        // {
        //     if (isCrouching)
        //     {
        //         animator.SetBool("isCrouching", false);
        //         isCrouching = false;
        //         transform.position = new Vector3(transform.position.x, transform.position.y-0.25f, transform.position.z);
        //     }
        //     else
        //     {
        //         animator.SetBool("isCrouching", true);
        //         isCrouching = true;
        //         transform.position = new Vector3(transform.position.x, transform.position.y+0.25f, transform.position.z);
        //     }
        // }

    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        GameController.instance.UpdateHealthBar();
    }

    protected override void Dead()
    {
        base.Dead();
        EventManager.instance.TriggerEvent(EventName.LevelFail);
    }

    private void ChangePlayerDirection(float direction)
    {
        if (direction > 0 && isFacingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
        else if (direction < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
        else if (direction > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
        else if (direction < 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.name == "House")
        {
            isStealth = true;
            var houseSprite = collision.GetComponent<SpriteRenderer>();
            houseSprite.color = new Color(houseSprite.color.r, houseSprite.color.g, houseSprite.color.b, .75f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collision.name == "House")
        {
            isStealth = false;
            var houseSprite = collision.GetComponent<SpriteRenderer>();
            houseSprite.color = new Color(houseSprite.color.r, houseSprite.color.g, houseSprite.color.b, 1f);
        }
    }

}
