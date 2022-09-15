using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    float _speedBooster = 1f;

    void Start()
    {
        InvokeRepeating(nameof(BoostDatSpeed), 30f, 30f);
    }

    void BoostDatSpeed()
    {
        if(gameObject.tag == "Obst")
        {
            if (_speedBooster < 2f)
            {
                _speedBooster *= 1.1f;
            }
        }
    }

    public float GetSpeedBooster()
    {
        return _speedBooster;
    }
}
