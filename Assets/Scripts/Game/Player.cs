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
    
    
    private void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var boxCollider = GetComponent<BoxCollider2D>();
        var capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isCrouching", true);
            
            if (Input.GetButtonDown("Horizontal"))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                boxCollider.offset = new Vector2(boxCollider.offset.x,boxCollider.offset.y + 0.08753417f);
                capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y + 0.9f);
            }
        }

        if(Input.GetButtonUp("Horizontal"))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", false);
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                boxCollider.offset = new Vector2(boxCollider.offset.x,boxCollider.offset.y - 0.08753417f);
                capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, capsuleCollider.offset.y - 0.9f);
            }
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
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.name is "House" or "Bush(Clone)")
        {
            isStealth = true;
            var objectSprite = collision.GetComponent<SpriteRenderer>();
            objectSprite.color = new Color(objectSprite.color.r, objectSprite.color.g, objectSprite.color.b, .75f);
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
            var objectSprite = collision.GetComponent<SpriteRenderer>();
            objectSprite.color = new Color(objectSprite.color.r, objectSprite.color.g, objectSprite.color.b, 1f);
        }
    }

}
