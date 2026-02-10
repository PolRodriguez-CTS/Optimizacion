using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Inputs
    private InputAction _shootAction;

    [Header("Shoot")]
    [SerializeField] private Transform _bulletTransform;
    [SerializeField] private GameObject _bulletPrefab;

    void Awake()
    {
        _shootAction = InputSystem.actions["LShoot"];
    }

    void Start()
    {}

    void Update()
    {
        if(_shootAction.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject("Bullets", _bulletTransform.position, _bulletTransform.rotation);
        bullet.SetActive(true);
    }
}