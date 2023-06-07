using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BushFire : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DisableFire());
    }

    private IEnumerator DisableFire()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            var rb = GetComponent<Rigidbody2D>();
            Destroy(rb);
        }
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Player"))
        {
            var character = col.gameObject.GetComponent<Character>();
            character.TakeDamage(1);
        }

    }
}
