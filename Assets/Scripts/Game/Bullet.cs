using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            var enemy = col.gameObject.GetComponent<Enemy>();
            enemy.isUnderAttack = true;
            enemy.TakeDamage(GameController.instance.currentSkill.damage);
            Destroy(gameObject);
        }

    }
}
