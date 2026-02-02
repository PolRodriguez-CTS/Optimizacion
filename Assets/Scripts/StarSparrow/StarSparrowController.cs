using UnityEngine;
using UnityEngine.InputSystem;

public class StarSparrowController : MonoBehaviour
{
    //Components
    private Rigidbody _rb;

    //Inputs
    private InputAction _moveAction;
    private Vector2 _moveInput;

    private InputAction _Lshoot;
    private InputAction _Rshoot;

    //Parameters
    private float _speed = 40f;

    [Header("Shoot")]
    [SerializeField] private Transform _LBulletTransform;
    [SerializeField] private Transform _RBulletTransform;
    [SerializeField] private GameObject _bulletPrefab;


    void Awake()
    {
        _moveAction = InputSystem.actions["Move"];
        _Lshoot = InputSystem.actions["LShoot"];
        _Rshoot = InputSystem.actions["RShoot"];

        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        //Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y); 
        
        Movement();

        if(_Lshoot.WasPressedThisFrame())
        {
            LShoot();
        }
        if(_Rshoot.WasPressedThisFrame())
        {
            RShoot();
        }
    }

    void Movement()
    {
        _rb.linearVelocity = _moveInput * _speed;
    }

    void LShoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject("Bullets", _LBulletTransform.position, _LBulletTransform.rotation);
        bullet.SetActive(true);
    }

    void RShoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject("Bullets", _RBulletTransform.position, _RBulletTransform.rotation);
        bullet.SetActive(true);
    }
}
