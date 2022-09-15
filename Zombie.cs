using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Zombie : MonoBehaviour
{
    [SerializeField] GameObject[] _zMeshes;
    [SerializeField] AudioClip[] _zSounds;
    [SerializeField] Slider _zSFXVolumeSlider;

    AudioSource _audioSource;

	Animator _animator;
    Player _player;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _player = FindObjectOfType<Player>();

        foreach (var mesh in _zMeshes)
        {
            mesh.SetActive(false);
        }

        _zMeshes[Random.Range(0, _zMeshes.Length)].SetActive(true);

        _animator.SetInteger("startInt", Random.Range(0, 7));
        _animator.SetTrigger("start");

        InvokeRepeating(nameof(PlayZombieSounds), Random.Range(0f, 3f), Random.Range(1f, 3f));

        _player.OhNo += MeatNomNomNom;
    }

    void Update()
    {
        PlayerPrefs.SetFloat("zSFXVolume", _zSFXVolumeSlider.value);
        _audioSource.volume = PlayerPrefs.GetFloat("zSFXVolume");
    }

    void MeatNomNomNom()
    {
        transform.DOMoveX(Random.Range(-5f, -3f), 2f);
    }

    void PlayZombieSounds()
    {
        _audioSource.PlayOneShot(_zSounds[Random.Range(0, _zSounds.Length)]);
    }
}
