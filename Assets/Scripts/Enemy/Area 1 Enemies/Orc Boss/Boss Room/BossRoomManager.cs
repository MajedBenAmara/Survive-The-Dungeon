using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    [SerializeField]    
    private BossRoomDoor[] _doors;
    [SerializeField]
    private SpriteRenderer _fogSpriteRenderer;
    internal bool playerEntered = false;
    [SerializeField]
    private GameObject _boss;
    private float _alpha;

    private void Start()
    {
        _alpha = _fogSpriteRenderer.color.a;
        //Debug.Log("alpha = " + _alpha);

    }
    private void Update()
    {
        CheckPlayer();
        CheckBoss();
    }
    private void CheckPlayer()
    {
        if (playerEntered)
        {
            foreach (var item in _doors)
            {
                item.CloseTheDoor();
            }
            StartCoroutine(FogCoroutine());
        }
    }
    private void CheckBoss()
    {
        if(_boss == null)
        {
            OpenAllDoors();
        }
    } 
    public void OpenAllDoors()
    {
        foreach (var item in _doors)
        {
            item.OpenTheDoor();
        }
    }

    IEnumerator FogCoroutine()
    {
        while(_alpha > 0)
        {
            _alpha -= .1f;

            if (_alpha < 0)
                _alpha = 0;

            _fogSpriteRenderer.color = new Color(_fogSpriteRenderer.color.r, _fogSpriteRenderer.color.g,
                _fogSpriteRenderer.color.b, _alpha);
            yield return new WaitForSeconds(.5f);
        }

    }
}

