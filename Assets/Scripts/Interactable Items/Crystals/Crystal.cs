using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Crystal : Interactibale
{
    [Header("Effect Elements")]
    [SerializeField]
    protected GameObject _doors; 
    [SerializeField]
    protected ParticleSystem _crystalParticleEffect;
    [SerializeField]
    protected GameObject _cleanCrystal;
    [SerializeField]
    protected Color startColor, endColor;

    [Header("Crystal Appearence")]
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;
    [SerializeField]
    protected float _colorChangingSpeed;
    [SerializeField]
    protected Light2D _lightSource;

    private bool _doItOnce = true;

    protected void HideCrystal()
    {
        Instantiate(_crystalParticleEffect, transform.position, Quaternion.identity);
        _spriteRenderer.enabled = false;
        _lightSource.enabled = false;
        if (!_crystalParticleEffect.isPlaying)
        {
            if(_doItOnce)
            {
                SfxManager.Instance.PlayDestroyCrystalSfx();
                Instantiate(_cleanCrystal, transform.position, Quaternion.identity);
                _doItOnce = false;
            }
        }
        _canInteract = false;
        //gameObject.SetActive(false);
    }
    protected void CloseDoor()
    {
        if(_doors != null)
            _doors.SetActive(true);

    }
    protected void OpenDoor()
    {
        if (_doors != null)
            _doors.SetActive(false);
       
    }
    // Change the color of the gem when player interact with it

    protected void ChangeCrystalColour()
    {
        SfxManager.Instance.PlayActivateCrystalSfx();
        GameManager.Instance.PlayerIsInteracting = true;

        _spriteRenderer.color = endColor;
        _lightSource.color = endColor;

        _HideInteractionButton = true;
    }



}
