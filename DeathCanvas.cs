using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] Image _deathIMG;
    [SerializeField] GameObject _yammyPanel;
    [SerializeField] TextMeshProUGUI _yammyTXT;
    [SerializeField] Button _toMenuBTN;

    Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    void Start()
    {
        //string[] _zombieTalks = { "yammy!", "brains!", "mmm, brains!", "fresh meat!", "one of us!" };
        string[] _zombieTalks = { "¬ ”—Õﬂÿ ¿!", "ÃŒ«√»»»»!", "ÕﬂÃ!", "Ãﬂ— Œ!", "LAST OF US!" };
        _yammyTXT.text = _zombieTalks[Random.Range(0, _zombieTalks.Length)];

        _toMenuBTN.onClick.AddListener(ILLBACK);

        _yammyPanel.transform.DOScale(0f, 0f);
        _yammyPanel.SetActive(false);
        _player.OhNo += Awwwfuckkk;
    }

    void Awwwfuckkk()
    {
        Sequence _deathSqc = DOTween.Sequence();
        _deathSqc.Append(_deathIMG.DOFade(1f, 2f));
        _yammyPanel.SetActive(true);
        _deathSqc.Append(_yammyPanel.transform.DOScale(1f, 1f));
    }

    void ILLBACK()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
