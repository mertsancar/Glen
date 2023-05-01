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
}
