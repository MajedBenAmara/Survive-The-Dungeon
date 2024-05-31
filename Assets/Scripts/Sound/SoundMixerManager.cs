using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager Instance;
    private static SoundMixerManager _instance;
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private Slider _masterVolumeSlider;
    [SerializeField]
    private Slider _soundFXVolumeSlider;
    [SerializeField]
    private Slider _musicVolumeSlider;

    [SerializeField]
    private float _soundFXVolume;
    [SerializeField]
    private float _masterVolume;
    [SerializeField]
    private float _musicVolume;


    private void Awake()
    {

        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Instance = this;
        LoadVolume();

    }

    private void Start()
    {
        SetSoundVolume();
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = volume;
        _audioMixer.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat("Master Volume", volume);
    }

    public void SetSoundFXVolume(float volume)
    {
        _soundFXVolume = volume;
        _audioMixer.SetFloat("soundFXVolume", volume);
        PlayerPrefs.SetFloat("SoundFX Volume", volume);

    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        _audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("Music Volume", volume);

    }

    public void LoadVolume()
    {
        _masterVolumeSlider.value =  PlayerPrefs.GetFloat("Master Volume");
        _soundFXVolumeSlider.value = PlayerPrefs.GetFloat("SoundFX Volume");
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume");
    }

    private void SetSoundVolume()
    {
        _audioMixer.SetFloat("masterVolume", _masterVolume);
        _audioMixer.SetFloat("soundFXVolume", _soundFXVolume);
        _audioMixer.SetFloat("musicVolume", _musicVolume);
    }

}
