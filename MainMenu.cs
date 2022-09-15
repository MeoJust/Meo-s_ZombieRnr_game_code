using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    Score _score;

    [SerializeField] GameObject _mainPanel;
    [SerializeField] GameObject _optionsPanel;
    [SerializeField] GameObject _chooseLevelPanel;
    [SerializeField] GameObject _backToMenuPanel;
    [SerializeField] GameObject _postProcesVolume;
    [SerializeField] ScriptableRendererFeature _ssao;

    [SerializeField] Button _tapBTN;
    [SerializeField] Button _startBTN;
    [SerializeField] Button _chooseLevelBTN;
    [SerializeField] Button _optionsBTN;
    [SerializeField] Button _quitBTN;
    [SerializeField] Button _backBTN;
    [SerializeField] Button _xBTN;
    [SerializeField] Button _yesBTN;
    [SerializeField] Button _noBTN;

    [SerializeField] Button _cityBTN;
    [SerializeField] Button _townBTN;

    [SerializeField] TextMeshProUGUI _townBTNLabelTXT;
    [SerializeField] TextMeshProUGUI _townBTNScoreTXT;

    [SerializeField] Toggle _ppSwitch;
    [SerializeField] Toggle _fogSwitch;
    [SerializeField] Toggle _ssaoSwitch;

    [SerializeField] GameObject _obstSpawner;

    public Action GameStarted;
    //public Action BackToStart;

    void Awake()
    {
        _score = FindObjectOfType<Score>();
    }

    void Start()
    {
        StartSettings();

        Time.timeScale = 1f;

        _backToMenuPanel.transform.DOScale(0f, 0f);
        _backToMenuPanel.SetActive(false);

        _tapBTN.transform.DOScale(1.2f, 1f).SetLoops(-1);

        BTNSsetup();
        SwitchesSetup();
        HideDatFcknX();

        FindObjectOfType<Player>().OhNo += HideDatFcknX;

        ChooseLevelBTNsSwitches(_townBTN, 1000, _townBTNScoreTXT, _townBTNLabelTXT);
    }

    void SwitchesSetup()
    {
        _ppSwitch.onValueChanged.AddListener(PostProcessSwitch);
        _fogSwitch.onValueChanged.AddListener(FogSwitch);
        _ssaoSwitch.onValueChanged.AddListener(SSAOSwitch);
    }

    void BTNSsetup()
    {
        _tapBTN.onClick.AddListener(ShowStartPanel);
        _startBTN.onClick.AddListener(StartDaGame);
        _chooseLevelBTN.onClick.AddListener(ChooseDaLevel);
        _optionsBTN.onClick.AddListener(OptionsOn);
        _backBTN.onClick.AddListener(OptionsOff);
        _quitBTN.onClick.AddListener(LetMeOuuuuuuuuuuuuut);
        _xBTN.onClick.AddListener(Pause);
        _yesBTN.onClick.AddListener(BackingToTheUSSR);
        _noBTN.onClick.AddListener(RunForestRun);

        _cityBTN.onClick.AddListener(() => ChoseLevel(0));
        _townBTN.onClick.AddListener(() => ChoseLevel(1));
    }

    void StartSettings()
    {
        RenderSettings.fog = PlayerPrefs.GetInt("isFogOn") != 0;
        _fogSwitch.isOn = PlayerPrefs.GetInt("isFogOn") != 0;

        _ssao.SetActive(PlayerPrefs.GetInt("isSSAOOn") != 0);
        _ssaoSwitch.isOn = PlayerPrefs.GetInt("isSSAOOn") != 0;

        _postProcesVolume.SetActive(PlayerPrefs.GetInt("isPPVOn") != 0);
        _ppSwitch.isOn = PlayerPrefs.GetInt("isPPVOn") != 0;
    }

    void ShowStartPanel()
    {
        _tapBTN.gameObject.SetActive(false);

        Sequence mainPanelStartSqnc = DOTween.Sequence();

        mainPanelStartSqnc.Append(_mainPanel.transform.DOLocalMoveY(-500f, 1f));
        mainPanelStartSqnc.Append(_mainPanel.transform.DOLocalMoveY(100f, .75f));
        mainPanelStartSqnc.Append(_mainPanel.transform.DOLocalMoveY(-50f, .5f));
        mainPanelStartSqnc.Append(_mainPanel.transform.DOLocalMoveY(-0f, .25f));
    }

    void StartDaGame()
    {
        Sequence mainPanelStartSqnc = DOTween.Sequence();

        mainPanelStartSqnc.Append(_mainPanel.transform.DOLocalMoveY(-100f, .5f));
        mainPanelStartSqnc.Append(_mainPanel.transform.DOLocalMoveY(1200f, 1f));

        _obstSpawner.SetActive(true);
        _xBTN.gameObject.SetActive(true);

        GameStarted.Invoke();
    }

    void ChooseDaLevel()
    {
        Sequence chooseLevelSqc = DOTween.Sequence();

        chooseLevelSqc.Append(_mainPanel.transform.DOLocalMoveY(-100f, .5f));
        chooseLevelSqc.Append(_mainPanel.transform.DOLocalMoveY(1200f, 1f));
        chooseLevelSqc.Append(_chooseLevelPanel.transform.DOLocalMoveY(100f, 1f));
        chooseLevelSqc.Append(_chooseLevelPanel.transform.DOLocalMoveY(0f, .5f));
    }

    void OptionsOn()
    {
        Sequence optionsSqc = DOTween.Sequence();

        optionsSqc.Append(_mainPanel.transform.DOLocalMoveY(-100f, .5f));
        optionsSqc.Append(_mainPanel.transform.DOLocalMoveY(1200f, 1f));
        optionsSqc.Append(_optionsPanel.transform.DOLocalMoveX(400f, 1f));
        optionsSqc.Append(_optionsPanel.transform.DOLocalMoveX(500f, .25f));
    }

    void OptionsOff()
    {
        Sequence optionsSqc = DOTween.Sequence();

        optionsSqc.Append(_optionsPanel.transform.DOLocalMoveX(400f, .25f));
        optionsSqc.Append(_optionsPanel.transform.DOLocalMoveX(1500f, 1f));
        optionsSqc.Append(_mainPanel.transform.DOLocalMoveY(-100f, 1f));
        optionsSqc.Append(_mainPanel.transform.DOLocalMoveY(0f, .5f));
    }

    void Pause()
    {
        IEnumerator CallOfCthu_Ehm_CallOfPause()
        {
            _backToMenuPanel.SetActive(true);
            _backToMenuPanel.transform.DOScale(1f, .5f);
            yield return new WaitForSeconds(.5f);
            Time.timeScale = 0;
        }
        StartCoroutine(CallOfCthu_Ehm_CallOfPause());
    }

    void BackingToTheUSSR()
    {
        //BackToStart.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ChoseLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    void RunForestRun()
    {
        IEnumerator Ruuuuuuuuuun()
        {
            Time.timeScale = 1f;
            _backToMenuPanel.transform.DOScale(0f, .5f);
            yield return new WaitForSeconds(.5f);
            _backToMenuPanel.SetActive(false);
        }
        StartCoroutine(Ruuuuuuuuuun());
    }

    void PostProcessSwitch(bool toggle)
    {
        PlayerPrefs.SetInt("isPPVOn", toggle ? 1 : 0);
        toggle = PlayerPrefs.GetInt("isPPVOn") != 0;
        _postProcesVolume.SetActive(toggle);
    }

    void FogSwitch(bool toggle)
    {
        PlayerPrefs.SetInt("isFogOn", toggle ? 1:0);
        toggle = PlayerPrefs.GetInt("isFogOn") != 0;
        RenderSettings.fog = toggle;
    }

    void SSAOSwitch(bool toggle)
    {
        PlayerPrefs.SetInt("isSSAOOn", toggle ? 1 : 0);
        toggle = PlayerPrefs.GetInt("isSSAOOn") != 0;
        _ssao.SetActive(toggle);
    }

    void HideDatFcknX()
    {
        _xBTN.gameObject.SetActive(false);
    }

    void ChooseLevelBTNsSwitches(Button button, int scoreNeeded, TextMeshProUGUI scoreNededTXT, TextMeshProUGUI labelTXT)
    {
        if(_score.GetTotalScore() < scoreNeeded)
        {
            button.enabled = false;
            labelTXT.gameObject.SetActive(false);
            scoreNededTXT.gameObject.SetActive(true);
        }
        else
        {
            button.enabled = true;
            labelTXT.gameObject.SetActive(true);
            scoreNededTXT.gameObject.SetActive(false);
        }
    }

    void LetMeOuuuuuuuuuuuuut()
    {
        Application.Quit();
    }
}
