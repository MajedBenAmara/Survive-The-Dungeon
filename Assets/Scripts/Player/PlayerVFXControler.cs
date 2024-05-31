using UnityEngine;

public class PlayerVFXControler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _slowEffect;

    [SerializeField]
    private ParticleSystem _rootEffect;

    [SerializeField]
    private ParticleSystem _drainEffect;

    [SerializeField]
    private ParticleSystem _fearEffect;

    [SerializeField]
    private ParticleSystem _healEffect;

    public void PlaySlowEffect()
    {
        _slowEffect.Play();
    }

    public void StopSlowEffect()
    {
        _slowEffect.Stop();
    }

    public void PlayRootEffect()
    {
        _rootEffect.Play();
    }

    public void StopRootEffect()
    {
        _rootEffect.Stop();
    }

    public void PlayDrainEffect()
    {
        _drainEffect.Play();
    }

    public void StopDrainEffect()
    {
        _drainEffect.Stop();
    }
    public void PlayFearEffect()
    {
        _fearEffect.Play();
    }

    public void StopFearEffect()
    {
        _fearEffect.Stop();
    }
    public void PlayHealEffect()
    {
        _healEffect.Play();
    }

}
