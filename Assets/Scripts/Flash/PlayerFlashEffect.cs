using System.Collections;
using UnityEngine;

public class PlayerFlashEffect : FlashEffect
{

    #region Variables

    [SerializeField]
    private SpriteRenderer _playerRenderer;
    [SerializeField]
    private Material _playerMaterial;

    [SerializeField]
    private Material _slowMaterial;

    [SerializeField]
    private Material _rootMaterial;

    [SerializeField]
    private Material _drainMaterial;

    #endregion

    protected override void Start()
    {
        _spriteRenderer = _playerRenderer;
        _originalMaterial = _playerMaterial;
    }

    #region Root Func
    public void RootFlash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        _flashRoutine = StartCoroutine(RootFlashRoutine());
        

    }
    private IEnumerator RootFlashRoutine()
    {
        // Swap to the flashMaterial.
        _spriteRenderer.material = _rootMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(FlashDuration);

        // After the pause, swap back to the original material.
        _spriteRenderer.material = _originalMaterial;
        //Debug.Log("Original material");
        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }

    #endregion

    #region Slow Func

    public void SlowFlash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }
        if (this.gameObject != null)
        {
            // Start the Coroutine, and store the reference for it.
            _flashRoutine = StartCoroutine(SlowFlashRoutine());
        }

    }
    private IEnumerator SlowFlashRoutine()
    {
        // Swap to the flashMaterial.
        _spriteRenderer.material = _slowMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(FlashDuration);

        // After the pause, swap back to the original material.
        _spriteRenderer.material = _originalMaterial;
        //Debug.Log("Original material");
        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }

    #endregion

    #region Drain Func

    public void DrainFlash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.

        _flashRoutine = StartCoroutine(DrainFlashRoutine());
        
    }
    private IEnumerator DrainFlashRoutine()
    {
        // Swap to the flashMaterial.
        _spriteRenderer.material = _drainMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(FlashDuration);

        // After the pause, swap back to the original material.
        _spriteRenderer.material = _originalMaterial;
        //Debug.Log("Original material");
        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }

    #endregion

}
