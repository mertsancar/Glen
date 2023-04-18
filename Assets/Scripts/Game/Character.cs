using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public int health;
    public bool isDead;
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
