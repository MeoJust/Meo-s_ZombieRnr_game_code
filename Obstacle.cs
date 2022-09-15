using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float _destroyTime;

    void Start()
    {
        Invoke(nameof(DieMthrfckrDie), _destroyTime);
    }

    void DieMthrfckrDie()
    {
        Destroy(gameObject);
    }
}
