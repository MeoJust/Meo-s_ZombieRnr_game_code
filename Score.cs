using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentScoreTXT;
    [SerializeField] TextMeshProUGUI _totalScoreTXT;
    [SerializeField] TextMeshProUGUI _hiScoreTXT;
    [SerializeField] Button _dollarBTN;

    bool _isGameStarted;
    float _currentScore = 0;
    float _hiScore = 0;
    float _totalScore = 0;

    public Action AdVieved;

    void Awake()
    {
        _hiScore = PlayerPrefs.GetFloat("hiScore");
        _totalScore = PlayerPrefs.GetFloat("totalScore");
    }

    void Start()
    {
        _dollarBTN.onClick.AddListener(ShowDatFcknAd);
        FindObjectOfType<MainMenu>().GameStarted += () => GameStartedBool(true);
        FindObjectOfType<MainMenu>().GameStarted += HideTotalScore;
        FindObjectOfType<Player>().OhNo += () => GameStartedBool(false);

        _totalScoreTXT.text = "общий счет: " + (int)_totalScore;
        //_totalScoreTXT.text = "total score: " + (int)_totalScore;

        _dollarBTN.transform.DOScale(1.2f, 1f).SetLoops(-1);
    }

    void Update()
    {
        _currentScoreTXT.text = "текущий счет: " + (int)_currentScore;
        //_currentScoreTXT.text = "current score: " + (int)_currentScore;
        _hiScoreTXT.text = "лучший счет: " + (int)PlayerPrefs.GetFloat("hiScore");
        //_hiScoreTXT.text = "high score: " + (int)PlayerPrefs.GetFloat("hiScore");

        if (PlayerPrefs.GetFloat("hiScore") < _currentScore)
        {
            PlayerPrefs.SetFloat("hiScore", _currentScore);
        }

        if (_isGameStarted)
        {
            _currentScore += Time.deltaTime;
            _totalScore += Time.deltaTime;
            PlayerPrefs.SetFloat("totalScore", _totalScore);
        }

        //TEMP
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetFloat("hiScore", 0);
            PlayerPrefs.SetFloat("totalScore", 0);
        }

    }

    void GameStartedBool(bool value)
    {
        _isGameStarted = value;
    }

    void HideTotalScore()
    {
        _totalScoreTXT.enabled = false;
        _dollarBTN.gameObject.SetActive(false);
    }

    void ShowDatFcknAd()
    {
        //show dat fckn ad
        AdReward();
    }

    void AdReward()
    {
        PlayerPrefs.SetFloat("totalScore", PlayerPrefs.GetFloat("totalScore") + 100f);
        _totalScore = PlayerPrefs.GetFloat("totalScore");
        _totalScoreTXT.text = "общий счет: " + (int)_totalScore;
        AdVieved.Invoke();
    }

    public float GetTotalScore() 
    { 
        return _totalScore; 
    }
}
