using System.Collections;
using UnityEngine;

public class BoxPuzzel : MonoBehaviour
{
    [SerializeField]
    private Box[] _boxList;
    private int boxCounter = 0;
    [SerializeField]
    private PuzzelCrystal _puzzelCrystal;
    private void Start()
    {
        foreach (var item in _boxList)
        {
            item.OnBoxDestruction += CheckDestroyedBox;
        }
    }

    private void CheckDestroyedBox(string boxId)
    {
        // every a box destroyed  we check the id of the player choice if is it the same one as in the list
        // with the correct order
        if (boxId == _boxList[boxCounter].PuzzelID)
        {
            _boxList[boxCounter].gameObject.SetActive(false);
            boxCounter++;
            if (boxCounter == _boxList.Length)
            {
                // Clear level
                _puzzelCrystal.StartCoroutine(_puzzelCrystal.PuzzelClearRoutine());
            }
        }
        else
        {
            StartCoroutine(OnLevelFailing());
        }

    }

    IEnumerator OnLevelFailing()
    {
        boxCounter = 0;
        yield return new WaitForSeconds(.1f);
        _puzzelCrystal.ResetCrystal();
        foreach (var item in _boxList)
        {
            item.gameObject.SetActive(true);
        }
    }
}
