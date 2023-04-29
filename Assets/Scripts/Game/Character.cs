using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected bool isDead;
    // public bool isUnderAttack;
    
    void Start()
    {
        isDead = false;
    }
    
    public virtual void GetDamage(int damageValue)
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
        gameObject.SetActive(false);
    }
}
