using System.Collections;
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

    //Disparo
    [SerializeField] private Transform _bulletTransform;
    private float shootTime = 1f;
    private float shootTimer;
    private bool canShoot = false;

    //Spawn
    [SerializeField] private Transform spawn;

    private float timePassed;

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
        Movement();
        Shoot();
    }

    

    void Movement()
    {
        //*Mejor almacenar en variable en vez de usar TIme.time pq si no no puedo reiniciar la posición al spawnear a los bichos
        timePassed += Time.fixedDeltaTime;

        //vertical
        float z = enemyVel * timePassed;

        //horizontal
        float x = Mathf.Sin(timePassed * waveFrequency) * waveAmplitude;


        Vector3 target = basePos + enemyDirection * z + waveDirection * x;

        _rb.MovePosition(target);
    }

    void Shoot()
    {
        //if(isDead) return;
        shootTimer += Time.fixedDeltaTime;

        if (shootTimer > shootTime)
        {
            canShoot = true;
            StartCoroutine(ShootCoroutine());
            shootTimer = 0f;
            return;
        }
    }

    IEnumerator ShootCoroutine()
    {
        if(!canShoot) yield return null;

        GameObject bullet = PoolManager.Instance.GetPooledObject("EnemyBullets", _bulletTransform.position, _bulletTransform.rotation);
        bullet.SetActive(true);
        canShoot = false;
        yield return null;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Death();
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        transform.position = EnemyManager.Instance.spawner.position;

        timePassed = 0f;
    }

}