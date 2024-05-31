using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePuzzelManager : MonoBehaviour
{
    [SerializeField]
    private List<SquarePuzzel> _squarePuzzels;
    [SerializeField]
    private PuzzelCrystal _puzzelCrystal;
    private int _puzzelClearedCounter = 0;
    private bool _itemFound = true;
    private int _numberOfItemInList;
    private bool _lvlFinished = false;
    private void Start()
    {
        foreach (var item in _squarePuzzels)
        {
            item.OnPuzzelTouch += HandleSquarePuzzel;
        }
        _numberOfItemInList = _squarePuzzels.Count;
    }

    private void Update()
    {
        OnLevelClear();
    }

    private void HandleSquarePuzzel(string id)
    {
        // search the object with the same id in the list
        foreach (var item in _squarePuzzels)
        {
            // when found extract it from the list

            if (item.PuzzelID == id)
            {
                if (!item.IsActivated)
                {
                    _puzzelClearedCounter++;
                    item.IsActivated = true;
                    //Debug.Log("Square " + item.PuzzelId + " Is removed");
                    _itemFound = true;
                    break;
                }
                else
                {
                    _itemFound = false;
                }

            }

        }
        // if it wasn't found -> Level is failed

        if (!_itemFound)
        {
            StartCoroutine(OnLevelFailing());
        }

    }

    private void ResetPuzzel()
    {
        foreach (var item in _squarePuzzels)
        {
            item.SwapSprite(item.FloopUp);
            item.IsActivated = false;
        }
        _puzzelClearedCounter = 0;
    }

    IEnumerator OnLevelFailing()
    {
        foreach (var item in _squarePuzzels)
        {
            item.SwapSprite(item.FloopUp);
        }
        yield return new WaitForSeconds(.5f);
        ResetPuzzel();
        yield return new WaitForSeconds(.1f);
        _puzzelCrystal.ResetCrystal();
    }

    private void OnLevelClear()
    {
        if (_itemFound && _puzzelClearedCounter >= _numberOfItemInList && !_lvlFinished)
        {
            //Debug.Log("Level is finished");
            _puzzelCrystal.StartCoroutine(_puzzelCrystal.PuzzelClearRoutine());
            _lvlFinished = true;
        }
        
    }
}
