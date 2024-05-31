using System;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    #region Variables

    public static SfxManager Instance;

    [Header("Player Sfx List")]
    [SerializeField]
    private SoundSO[] _playerSfx;

    [Header("Crystal Sfx List")]
    [SerializeField]
    private SoundSO[] _CrystalSfx;

    [Header("Enemy Crystal Sfx List")]
    [SerializeField]
    private SoundSO[] _enemyCrystalSfx;

    [Header("Puzzel Crystal Sfx List")]
    [SerializeField]
    private SoundSO[] _puzzelCrystalSfx;

    [Header("Game Sfx List")]
    [SerializeField]
    private SoundSO[] _gameSFX;


    [Header("Audio Source")]
    [SerializeField]
    private AudioSource _audioSource;

    #endregion

    #region Unity Func

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Built Func

    #region General Funcs
    // Find a specific Sfx by giving it's name and the list it belong to
    private SoundSO FindSfx(SoundSO[] soundList, string clipName)
    {
        SoundSO result = Array.Find(soundList, x => x.AudioName == clipName);
        return result;
    }

    // return the length of a player sfx clip
    public float PlayerSfxClipLength(string SfxName)
    {
        return FindSfx(_playerSfx, SfxName).Clip.length;
    }

    // Play a specific Sfx by giving it's name and the list it belong to
    private void PlaySfx(string audioName, SoundSO[] sfxList)
    {
        // Find the sfx clip in the givin list with the provided name
        SoundSO audioClip = FindSfx(sfxList, audioName);
        // Create an audio source in the scene with the clip that we found
        AudioSource audioSource = Instantiate(_audioSource, transform.position, Quaternion.identity);
        // We assign the clip and it's volume to the audio source
        audioSource.clip = audioClip.Clip;
        audioSource.volume = audioClip.Volume;
        // we play the clip
        audioSource.Play();
        // we destroy the audio source when the clip end playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    #endregion

    #region Player Sfx Func

    public void PlaySlashSfx(int index)
    {
        PlaySfx("Sword Slash " + index, _playerSfx);
    }

    public void PlayWalkingSfx()
    {
        PlaySfx("Knight Walking", _playerSfx);
    }

    public void PlayConsumePotionSfx()
    {
        PlaySfx("Consume Potion", _playerSfx);
    }

    public void PlayGrabItemSfx()
    {
        PlaySfx("Grab Item", _playerSfx);
    }

    public void PlayPlayerDeathSfx()
    {
        PlaySfx("Player Death", _playerSfx);
    }

    public void PlayPlayerGettingHitSfx()
    {
        PlaySfx("Player Getting Hit", _playerSfx);
    }

    #endregion

    #region Crystals SFX Func
    public void PlayActivateCrystalSfx()
    {
        PlaySfx("Activate Crystal", _CrystalSfx);
    }

    public void PlayDestroyCrystalSfx()
    {
        PlaySfx("Destroy Crystal", _CrystalSfx);
    }
    #endregion

    #region Enemy Crystal SFX Func

    public void PlaySpawnEnemySfx()
    {
        PlaySfx("Spawn Enemy", _enemyCrystalSfx);
    }

    #endregion

    #region Puzzel Crystal SFX Func

    public void PlayBoxSmashedSfx()
    {
        PlaySfx("Box Smashed", _puzzelCrystalSfx);
    }

    public void PlayDropPotionSfx()
    {
        PlaySfx("Drop Potion", _puzzelCrystalSfx);
    }
    public void PlayLightSmallCrystalSfx()
    {
        PlaySfx("Light Small Crystal", _puzzelCrystalSfx);
    }
    public void PlayPressFloorSfx()
    {
        PlaySfx("Press Floor", _puzzelCrystalSfx);
    }
    public void PlayWrongAnswerSfx()
    {
        PlaySfx("Wrong Answer", _puzzelCrystalSfx);
    }

    #endregion

    #region Buttons SFX

    public void PlayButtonSfx()
    {
        PlaySfx("Button Pressed", _gameSFX);
    }

    #endregion

    #region Chest SFX

    public void PlayCoinSfx()
    {
        PlaySfx("Coin Collected", _gameSFX);
    }

    #endregion

    #endregion
}
