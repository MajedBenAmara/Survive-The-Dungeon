using System.Collections;
using UnityEngine;

public class PuzzelCrystal : Crystal
{
    [SerializeField]
    private GameObject _puzzelGameObject;
    [SerializeField]
    private GameObject _potion;

    private void Start()
    {
        _puzzelGameObject.SetActive(false);
    }
    private void Update()
    {
        ShowInteractionButton();
        CheckPlayerInteraction();
    }

    protected override void InteractWithPlayer()
    {

        ChangeCrystalColour();
        _puzzelGameObject.SetActive(true);
        if(_doors != null)
        {
            CloseDoor();
        }

    }

    public void ResetCrystal()
    {
        //Debug.Log("Color Reset");
        OpenDoor();
        SfxManager.Instance.PlayWrongAnswerSfx();
        _spriteRenderer.color = startColor;
        _lightSource.color = startColor;
        _HideInteractionButton = false;
        _puzzelGameObject.SetActive(false);
    }

    public IEnumerator PuzzelClearRoutine()
    {
        yield return new WaitForSeconds(.3f);
        OpenDoor();
        _puzzelGameObject.SetActive(false);
        SfxManager.Instance.PlayDropPotionSfx();    
        Instantiate(_potion, transform.position, Quaternion.identity);
        HideCrystal();
    }

}
