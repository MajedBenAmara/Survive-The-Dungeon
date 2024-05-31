using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBossWeaponHitBox : MonoBehaviour
{
    OgreBossStateManager _bosstateManager;
    private float _hitMoment;

    private void Start()
    {
        _bosstateManager = GetComponentInParent<OgreBossStateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void DealDamaget(Collider2D collision)
    {
        if (Time.time - _hitMoment > _bosstateManager.TimeBetweenMeleeAttacks)
        {
            _hitMoment = Time.time;
            _bosstateManager.DealDamage(_bosstateManager.WeaponDamage);
        }
    }
}
