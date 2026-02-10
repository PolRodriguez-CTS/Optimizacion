using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rb;

    //Movimiento vertical
    private float enemyVel = 10f;
    private Vector3 enemyDirection = new Vector3(0, -1, 0);

    //Movimiento horizontal
    private Vector3 waveDirection = new Vector3(1, 0, 0);
    private float waveAmplitude = 20f;
    private float waveFrequency = 2f;

    //Centro de la onda
    private Vector3 basePos;

    //Muerte
    private bool isDead = false;

    //Disparo
    [SerializeField] private Transform _bulletTransform;
    private float shootTime = 2f;
    private float shootTimer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }
    void Start()
    {
        basePos = transform.position;
    }

    void FixedUpdate()
    {
        if(isDead) return;
        Movement();
        Shoot();
    }

    void Movement()
    {
        //Bajada
        float z = enemyVel * Time.fixedTime;

        //Onda
        float x = Mathf.Sin(Time.fixedTime * waveFrequency) * waveAmplitude;

        Vector3 position = basePos + x * waveDirection + z * enemyDirection;
        _rb.MovePosition(position);
    }

    void Shoot()
    {
        if(isDead) return;

        shootTimer += Time.fixedDeltaTime;
        Debug.Log("Disparando");

        if (shootTimer > shootTime)
        {
            Debug.Log("Reiniciando timer");
            shootTimer = 0f;
            return;
        }
        
        GameObject bullet = PoolManager.Instance.GetPooledObject("EnemyBullets", _bulletTransform.position, _bulletTransform.rotation);
        bullet.SetActive(true);
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
