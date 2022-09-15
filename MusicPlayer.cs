using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] _thisIsMetalMthrFckr;
    [SerializeField] Slider _musicVolumeSlider;
    [SerializeField] Slider _sfxVolumeSlider;

    AudioSource _musicSource;

    void Awake()
    {
        _musicSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

        _musicSource.clip = _thisIsMetalMthrFckr[Random.Range(0, _thisIsMetalMthrFckr.Length)];
        _musicSource.Play();
    }

    void Update()
    {
        PlayerPrefs.SetFloat("musicVolume", _musicVolumeSlider.value);
        _musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    public float SfxVolume()
    {
        return _sfxVolumeSlider.value;
    }
}
