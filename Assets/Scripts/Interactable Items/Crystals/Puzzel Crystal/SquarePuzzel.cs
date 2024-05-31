using System;
using UnityEngine;

public class SquarePuzzel : Puzzel
{
    public event Action<string> OnPuzzelTouch;
    internal bool IsActivated = false;
    public Sprite FloopUp;
    public Sprite FloopDown;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GenerrateID();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.activeSelf)
        {
            //Debug.Log("Pzzel Id = " + PuzzelId);
            SfxManager.Instance.PlayPressFloorSfx();
            SwapSprite(FloopDown);
            OnPuzzelTouch?.Invoke(PuzzelID);
        }
    }

    public void SwapSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

}
