using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    //Components
    private Rigidbody _rb;

    //Inputs
    private InputAction _moveAction;
    private Vector2 _moveInput;

    private InputAction _Lshoot, _Rshoot;

    //Parameters
    private float _speed = 40f;

    [Header("Shoot")]
    [SerializeField] private Transform _LBulletTransform;
    [SerializeField] private Transform _RBulletTransform;
    [SerializeField] private GameObject _bulletPrefab;

    private Vector3 maxPosition = new Vector3(20, 45, 0);
    private Vector3 minPosition = new Vector3(-20, -40, 0);


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
        
        Movement();

        if(_Lshoot.WasPressedThisFrame())
        {
            Shoot(_LBulletTransform);
        }
        if(_Rshoot.WasPressedThisFrame())
        {
            Shoot(_RBulletTransform);
        }
    }

    void FixedUpdate()
    {
        float clampX = Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x);
        float clampY = Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y);

        transform.position = new Vector3(clampX, clampY, 0);
    }

    void Movement()
    {
        _rb.linearVelocity = _moveInput * _speed;
    }

    void Shoot(Transform canon)
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject("Bullets", canon.position, canon.rotation);
        bullet.SetActive(true);
    }
}
