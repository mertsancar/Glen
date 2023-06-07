using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireSplash : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DisableFire());
    }

    private IEnumerator DisableFire()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() => Destroy(gameObject));
    }


}
