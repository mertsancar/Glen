using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{

    void Start()
    {
        // var x = GameObject.FindWithTag("Player").transform;
        // StartCoroutine(PrepareBullet(x));
    }
    

    private IEnumerator PrepareBullet(Transform _playerPos)
    {
        while (true)
        {
            // some shooting code...
            yield return new WaitForSeconds(1f);
        }
    }
}
