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
    
    private bool isCrouching = false;
    private bool isLadder = false;
    private bool isClimbing = false;
    
    
    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Input.GetAxisRaw("Vertical") * speed / 2);
        }
        else
        {
            rigidbody2D.gravityScale = 4f;
        }
    }

    private void Movement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var boxCollider = GetComponent<BoxCollider2D>();
        var capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        else if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isCrouching", true);
            
            if (Input.GetButtonDown("Horizontal"))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                boxCollider.offset = new Vector2(boxCollider.offset.x,boxCollider.offset.y);
                capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y);
            }
        }
        else if (!Input.GetButton("Horizontal"))
        {
            animator.SetBool("isCrouching", false);
        }
        

        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y) ;
        ChangePlayerDirection(horizontal);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }

    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        GameController.instance.UpdateHealthBar();
    }

    protected override void Dead()
    {
        base.Dead();
        EventManager.instance.TriggerEvent(EventName.ShowScreenRequested, typeof(LevelFailScreen), null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
        }
        if (collision.name is "House" or "Bush(Clone)")
        {
            isStealth = true;
            GetComponent<Animator>().SetBool("isCrouching", true);
            var objectSprite = collision.GetComponent<SpriteRenderer>();
            objectSprite.color = new Color(objectSprite.color.r, objectSprite.color.g, objectSprite.color.b, .75f);
        }

        if (collision.CompareTag("Vine"))
        {
            isLadder = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.name is "House" or "Bush(Clone)")
        {
            isStealth = false;
            GetComponent<Animator>().SetBool("isCrouching", false);
            var objectSprite = collision.GetComponent<SpriteRenderer>();
            objectSprite.color = new Color(objectSprite.color.r, objectSprite.color.g, objectSprite.color.b, 1f);
        }
        if (collision.CompareTag("Vine"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

}
