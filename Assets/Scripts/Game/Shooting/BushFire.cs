using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BushFire : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DisableBush());
    }

    private IEnumerator DisableBush()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            var enemy = col.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(1);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            var player = col.gameObject.GetComponent<Player>();
            player.TakeDamage(1);
        }
    }
}
