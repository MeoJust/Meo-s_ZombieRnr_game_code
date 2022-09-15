using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] _meshes;

    [SerializeField] float _jumpForce;
    [SerializeField] Collider _baseCollider;
    [SerializeField] ParticleSystem _runDustFX;
    [SerializeField] ParticleSystem _jumpDustFX;
    [SerializeField] TextMeshProUGUI _survivorsTXT;

    Rigidbody _rb;
    bool _isOnGround;
    bool _isAlive;
    Animator[] _animators;
    Collider[] _ragDollColliders;
    Rigidbody[] _ragDollRbs;

    Score _score;
    MainMenu _mainMenu;

    public Action OhNo;

    void Awake()
    {
        _score = FindObjectOfType<Score>();
        _mainMenu = FindObjectOfType<MainMenu>();
    }

    void Start()
    {
        MeshesOpened();

        _rb = GetComponent<Rigidbody>();
        _animators = GetComponentsInChildren<Animator>();
        _ragDollColliders = GetComponentsInChildren<Collider>();
        _ragDollRbs = GetComponentsInChildren<Rigidbody>();
        _isAlive = true;

        RagdollsOnOff(false);

        _rb.useGravity = true;
        _baseCollider.enabled = true;

        SurvTXTUpdate();
        _score.AdVieved += SurvTXTUpdate;
        _survivorsTXT.transform.DOScale(1.1f, 1f).SetLoops(-1);

        _mainMenu.GameStarted += SurvTXToff;
    }

    void SurvTXTUpdate()
    {
        if ((int)_score.GetTotalScore() / 100 <= _meshes.Length)
        {
            _survivorsTXT.text = "ÂÛÆÈÂØÈÅ: " + (((int)_score.GetTotalScore() / 100) +1) + "/" + (_meshes.Length + 1);
        }
        else
        {
            _survivorsTXT.text = "ÂÛÆÈÂØÈÅ: " + (_meshes.Length + 1) + "/" + (_meshes.Length + 1);
        }
        //_survivorsTXT.text = "survivors: " + ((int)_score.GetTotalScore() / 100) + "/" + _meshes.Length;
    }

    void Update()
    {
        JumpMFCKR();
    }

    void RagdollsOnOff(bool value)
    {
        foreach (var rb in _ragDollRbs)
        {
            rb.useGravity = value;
        }

        foreach (var collider in _ragDollColliders)
        {
            collider.enabled = value;
        }
    }

    void MeshesOpened()
    {
        if (_meshes.Length > _score.GetTotalScore() / 100)
        {
            _meshes[UnityEngine.Random.Range(0, (int)_score.GetTotalScore() / 100)].SetActive(true);
        }
        else
        {
            _meshes[UnityEngine.Random.Range(0, _meshes.Length)].SetActive(true);
        }
    }

    void JumpMFCKR()
    {
        if (Input.GetMouseButtonDown(0) && _isOnGround)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isOnGround = false;

            foreach (var animator in _animators)
            {
                animator.SetBool("isJumping", true);
            }

            _runDustFX.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isOnGround = true;

            foreach (var animator in _animators)
            {
                animator.SetBool("isJumping", false);
            }

            _runDustFX.gameObject.SetActive(true);
            _jumpDustFX.Play();
        }

        if(collision.gameObject.tag == "Obst" && _isAlive)
        {
            foreach (var animator in _animators)
            {
                animator.enabled = false;
            }

            RagdollsOnOff(true);

            _baseCollider.enabled = false;
            OhNo.Invoke();
            _isAlive = false;
            Invoke(nameof(BaseColliderSwitch), .2f);
        }
    }

    void BaseColliderSwitch()
    {
        _baseCollider.enabled = true;
    }

    void SurvTXToff()
    {
        _survivorsTXT.gameObject.SetActive(false);
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isOnGround = false;

            foreach (var animator in _animators)
            {
                animator.SetBool("isJumping", true);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isOnGround = true;

            foreach (var animator in _animators)
            {
                animator.SetBool("isJumping", false);
            }
        }
    }
}
