using System.Collections;
using UnityEngine;

public class LightPuzzel : MonoBehaviour
{
    [SerializeField]
    private SmallPuzzelCrystal[] _smallCrystalsList;
    [SerializeField]
    private PuzzelCrystal _puzzelCrystal;
    private int counter = 0;
    private bool puzzelFailed = false;
    private bool _showSequenceOnce = true;

    private void OnEnable()
    {
        counter = 0;

    }

    private void Update()
    {
        if(_showSequenceOnce)
        {
            StartCoroutine(ActivatePuzzelCoroutine());

            _showSequenceOnce = false;
        }
    }
    public void ChecKPuzzelSolution(string id)
    {
        // if the id of the player choice not the same one as in the list with the correct order
        // then we fail the player
        if(id != _smallCrystalsList[counter].PuzzelID)
        {
            puzzelFailed = true;
        }

        counter++;
/*        Debug.Log("Counter = "  + counter);
*/
        // if we reach the end of the list 
        if(counter == _smallCrystalsList.Length)
        {
            // we check did the player find the correct solution

            // if he failed
            if(puzzelFailed)
            {
                counter = 0;
                puzzelFailed = false;
                // we start the fail coroutine
                StartCoroutine(PuzzelFailedCoroutine());
            }
            else
            {
                // else we start the clear coroutine
                StartCoroutine(PuzzelClearCoroutine());
            }
        }
    }

    // Show the player the correct order to clear this puzzel at the start
    private IEnumerator ActivatePuzzelCoroutine()
    {
        foreach (var item in _smallCrystalsList)
        {
            item.CanInteract = false;
            item.ChangeLightIntensity(item.LightHighIntensity);
            yield return new WaitForSeconds(.5f);
            item.ChangeLightIntensity(item.LightLowIntensity);
            yield return new WaitForSeconds(.2f);
        }
        // give the player the ability to interact with the small crystall after we show him the order
        foreach (var item in _smallCrystalsList)
        {
            item.CanInteract = true;
        }
    }

    private IEnumerator PuzzelClearCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        foreach (var item in _smallCrystalsList)
        {
            item.ChangeCrystalLightColor(Color.green);
            item.ChangeCrystalSpriteColor(Color.green);
        }
        yield return new WaitForSeconds(.3f);
        _puzzelCrystal.StartCoroutine(_puzzelCrystal.PuzzelClearRoutine());
    }


    private IEnumerator PuzzelFailedCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        foreach (var item in _smallCrystalsList)
        {
            item.CanInteract = false;
            item.ChangeCrystalLightColor(Color.red);
            item.ChangeCrystalSpriteColor(Color.red);
        }
        yield return new WaitForSeconds(.3f);
        foreach (var item in _smallCrystalsList)
        {
            ResetSmallCrystall(item);
        }
        _showSequenceOnce = true; 
        _puzzelCrystal.ResetCrystal();
    }

    private void ResetSmallCrystall(SmallPuzzelCrystal smallCrystal)
    {
        smallCrystal.ChangeLightIntensity(smallCrystal.LightLowIntensity);
        smallCrystal.ChangeCrystalSpriteColor(smallCrystal.OriginalColor);
        smallCrystal.ChangeCrystalLightColor(smallCrystal.OriginalColor);
    }
}
