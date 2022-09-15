using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed;

    SpeedBooster _booster;

    void Awake()
    {
        _booster = FindObjectOfType<SpeedBooster>();
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * (_speed * _booster.GetSpeedBooster()) * Vector3.left);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
