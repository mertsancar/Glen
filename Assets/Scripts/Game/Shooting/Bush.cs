using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bush : MonoBehaviour
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
        if (col.gameObject.CompareTag("Ground"))
        {
            GetComponent<Animator>().SetBool("Genarate", true);
            var rb = GetComponent<Rigidbody2D>();
            Destroy(rb);
        }
    }
}
