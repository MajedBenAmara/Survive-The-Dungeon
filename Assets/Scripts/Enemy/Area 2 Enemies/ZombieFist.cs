using System;
using UnityEngine;

public class ZombieFist : MonoBehaviour
{
    public static event Action FearThePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FearThePlayer?.Invoke();
        }
    }
}
