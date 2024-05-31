using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollCoinNotification : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
