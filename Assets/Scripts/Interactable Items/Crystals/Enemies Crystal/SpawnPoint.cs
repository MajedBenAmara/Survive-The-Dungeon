using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    #region Variables
    public int CreatedEnemiesCounter = 0;
    [Header("Enemy Number")]
    public int SpawnedEnemyNumber = 5;

    [Header("Enemy Type")]
    [SerializeField]
    private Enemy _enemy;
    [Header("Spawn Cooldown")]
    [SerializeField]
    private float _timeBetweenSpawning = 1f;
    [Header("Effect")]
    [SerializeField]
    private ParticleSystem _smallGemEffect;

    private Animator _anim;
    #endregion

    #region Unity Func

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(SpawnEnemyCoroutine());
    }
    #endregion

    #region Built Func
    IEnumerator SpawnEnemyCoroutine()
    {
        _anim.Play("Summoning");
        //Debug.Log("Enemies instaniated = " + _createdEnemiesCounter);
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Summoning")
            && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f)
        {
            //Debug.Log("Enemies Spawned");
            SpawnEnemy();
            yield return new WaitForSeconds(_timeBetweenSpawning);
        }
        else
        {
            yield return null;
        }

        if (CreatedEnemiesCounter < SpawnedEnemyNumber)
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }
    }

    private void SpawnEnemy()
    {
        SfxManager.Instance.PlaySpawnEnemySfx();
        Instantiate(_enemy, transform.position, transform.rotation);
        CreatedEnemiesCounter++;
        _anim.Play("Idle");
    }

    public void ActivateDestructionEffect()
    {
        Instantiate(_smallGemEffect, transform.position, Quaternion.identity);
    }
    #endregion

}
