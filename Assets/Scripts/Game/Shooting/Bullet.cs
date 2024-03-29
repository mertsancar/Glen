using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject fireSplashPrefab;
    public GameObject bushFirePrefab;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        // if (col.gameObject.CompareTag("Enemy"))
        // {
        //     var enemy = col.gameObject.GetComponent<Enemy>();
        //     enemy.isUnderAttack = true;
        //     enemy.TakeDamage(GameController.instance.currentSkill.damage);
        // }
        // if (col.gameObject.CompareTag("Player"))
        // {
        //     var player = col.gameObject.GetComponent<Player>();
        //     player.TakeDamage(GameController.instance.currentSkill.damage);
        // }
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Wall"))
        {
            var position = transform.position;
            var go = Instantiate(bushFirePrefab, new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.identity);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bush"))
        {
            var position = transform.position;
            var go = Instantiate(bushFirePrefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
