using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Puzzel
{
    public event Action<string> OnBoxDestruction;

    private float hitMoment;
    private float timeBetweenHits = .5f;
    private FlashEffect _destructionRffect;
    private void Start()
    {
        _destructionRffect = GetComponent<FlashEffect>();
        GenerrateID();
    }

    // Add a gap between the time when the player hit the box and the start the Destruction Coroutine
    public void HandleWeaponHit()
    {
        if(Time.time - hitMoment > timeBetweenHits)
        {
            hitMoment = Time.time;
            StartCoroutine(DestructionCoroutine());
        }  
    }

    IEnumerator DestructionCoroutine()
    {
        SfxManager.Instance.PlayBoxSmashedSfx();
        _destructionRffect.NormalFlash();
        yield return new WaitForSeconds(_destructionRffect.FlashDuration);
        OnBoxDestruction?.Invoke(PuzzelID);
        gameObject.SetActive(false);
    }
}
