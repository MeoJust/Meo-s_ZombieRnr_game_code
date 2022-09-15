using UnityEngine;

public class SFXMaker : MonoBehaviour
{
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    void Start()
    {
        PlayerPrefs.SetFloat("sfxVolume", FindObjectOfType<MusicPlayer>().SfxVolume());
        _audioSource.volume = PlayerPrefs.GetFloat("sfxVolume");
    }
}
