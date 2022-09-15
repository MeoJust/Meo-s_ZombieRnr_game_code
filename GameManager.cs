using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Player _player;

    [SerializeField] Light _sun;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _player = FindObjectOfType<Player>();

        RenderSettings.fogDensity = Random.Range(0.01f, 0.05f);

        _sun.transform.localRotation = transform.localRotation * Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 360), 0f);

        _player.OhNo += PlayerRIP;
    }

    void PlayerRIP()
    {
        Mover[] movers = FindObjectsOfType<Mover>();
        ObstSpawner[] obstSpawners = FindObjectsOfType<ObstSpawner>();

        foreach (var mover in movers)
        {
            mover.SetSpeed(0f);
        }

        foreach (var obstSpawner in obstSpawners)
        {
            Destroy(obstSpawner.gameObject);
        }
    }
}
