using System.Collections;
using UnityEngine;

public class EnemyFlashEffect : FlashEffect
{
    #region Editor Settings

    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material _burnFlashMaterial;
    [SerializeField] private Material _electrocutedFlashMaterial;


    #endregion


    #region Methods

    #region Burn Flash

    public void BurnFlash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        _flashRoutine = StartCoroutine(BurnFlashRoutine());
    }
    private IEnumerator BurnFlashRoutine()
    {
        // Swap to the flashMaterial.
        _spriteRenderer.material = _burnFlashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(FlashDuration);

        // After the pause, swap back to the original material.
        _spriteRenderer.material = _originalMaterial;
        //Debug.Log("Original material");
        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }

    #endregion

    #region Electro Flash

    public void ElectroFlash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        _flashRoutine = StartCoroutine(ElectroFlashRoutine());
    }

    private IEnumerator ElectroFlashRoutine()
    {
        // Swap to the flashMaterial.
        _spriteRenderer.material = _electrocutedFlashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(FlashDuration);

        // After the pause, swap back to the original material.
        _spriteRenderer.material = _originalMaterial;
        //Debug.Log("Original material");
        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }

    #endregion

    #endregion

}
