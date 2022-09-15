using UnityEngine;

public class ObstSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _objs;

    [SerializeField] float _spawnStartDelayMin = 2f;
    [SerializeField] float _spawnStartDelayMax = 2f;
    [SerializeField] float _spawnRepeatRateMin = 2f;
    [SerializeField] float _spawnRepeatRateMax = 2f;
    [SerializeField] float _spawnXmin = 12f;
    [SerializeField] float _spawnXmax = 20f;
    [SerializeField] float _spawnY = 0f;
    [SerializeField] float _spawnZ = 2.5f;

    void Start()
    {
        SpawnRepeat();
    }

    void SpawnDatBiches()
    {
        Instantiate(
            _objs[Random.Range(0, _objs.Length)], 
            new Vector3(Random.Range(_spawnXmin, _spawnXmax), _spawnY, _spawnZ), 
            new Quaternion(0, 0, 0, 0));
    }

    void SpawnRepeat()
    {
        InvokeRepeating(
            nameof(SpawnDatBiches),
            Random.Range(_spawnStartDelayMin, _spawnStartDelayMax),
            Random.Range(_spawnRepeatRateMin, _spawnRepeatRateMax));
    }
}
