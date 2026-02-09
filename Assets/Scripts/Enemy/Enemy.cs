using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rb;
    private float enemyVel = 20f;
    private Vector3 enemyDirection = new Vector3(0, -1, 0);
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = enemyDirection * enemyVel;
    }
}
