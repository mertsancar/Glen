using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;

    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected bool isDead;
    protected bool isAttacking = false;
    public bool isUnderAttack;
    
    private bool isFacingRight = true;
    public int GetHealth() => health;
    
    void Start()
    {
        isDead = false;
    }
    
    public virtual void TakeDamage(int damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
    
    protected void ChangePlayerDirection(float direction)
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
}
